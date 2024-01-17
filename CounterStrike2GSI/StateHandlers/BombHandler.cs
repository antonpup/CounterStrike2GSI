using CounterStrike2GSI.EventMessages;
using CounterStrike2GSI.Nodes;
using System.Collections.Generic;

namespace CounterStrike2GSI
{
    public class BombHandler : EventHandler<CS2GameEvent>
    {
        private Dictionary<string, Player> _player_cache = new Dictionary<string, Player>();

        public BombHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<PlayerUpdated>(OnPlayerUpdated);
            dispatcher.Subscribe<BombUpdated>(OnBombUpdated);
            dispatcher.Subscribe<BombStateUpdated>(OnBombStateUpdated);
        }

        ~BombHandler()
        {
            dispatcher.Unsubscribe<PlayerUpdated>(OnPlayerUpdated);
            dispatcher.Unsubscribe<BombUpdated>(OnBombUpdated);
            dispatcher.Unsubscribe<BombStateUpdated>(OnBombStateUpdated);
        }

        private void OnPlayerUpdated(CS2GameEvent e)
        {
            PlayerUpdated evt = (e as PlayerUpdated);

            if (evt == null)
            {
                return;
            }

            _player_cache[evt.PlayerID] = evt.New;
        }

        private void OnBombUpdated(CS2GameEvent e)
        {
            BombUpdated evt = (e as BombUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.State.Equals(evt.Previous.State))
            {
                dispatcher.Broadcast(new BombStateUpdated(evt.New.State, evt.Previous.State));

                switch (evt.New.State)
                {
                    case Nodes.BombState.Carried:
                        dispatcher.Broadcast(new BombPickedup(_player_cache[evt.New.Player]));
                        break;
                    case Nodes.BombState.Planting:
                        dispatcher.Broadcast(new BombPlanting(_player_cache[evt.New.Player]));
                        break;
                    case Nodes.BombState.Defusing:
                        dispatcher.Broadcast(new BombDefusing(_player_cache[evt.New.Player]));
                        break;
                }
            }
        }

        private void OnBombStateUpdated(CS2GameEvent e)
        {
            BombStateUpdated evt = (e as BombStateUpdated);

            if (evt == null)
            {
                return;
            }

            switch (evt.New)
            {
                case Nodes.BombState.Dropped:
                    dispatcher.Broadcast(new BombDropped());
                    break;
                case Nodes.BombState.Planted:
                    dispatcher.Broadcast(new BombPlanted());
                    break;
                case Nodes.BombState.Defused:
                    dispatcher.Broadcast(new BombDefused());
                    break;
                case Nodes.BombState.Exploded:
                    dispatcher.Broadcast(new BombExploded());
                    break;
            }
        }
    }
}
