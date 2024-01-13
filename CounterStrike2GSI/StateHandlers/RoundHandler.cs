using CounterStrike2GSI.EventMessages;
using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI
{
    public class RoundHandler : EventHandler<CS2GameEvent>
    {
        private Map _map = new Map();
        public RoundHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<RoundUpdated>(OnRoundUpdated);
            dispatcher.Subscribe<MapUpdated>(OnMapUpdated);
        }

        ~RoundHandler()
        {
            dispatcher.Unsubscribe<RoundUpdated>(OnRoundUpdated);
            dispatcher.Unsubscribe<MapUpdated>(OnMapUpdated);
        }

        private void OnMapUpdated(CS2GameEvent e)
        {
            MapUpdated evt = (e as MapUpdated);

            if (evt == null)
            {
                return;
            }

            _map = evt.New;
        }

        private void OnRoundUpdated(CS2GameEvent e)
        {
            RoundUpdated evt = (e as RoundUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.Phase.Equals(evt.Previous.Phase))
            {
                dispatcher.Broadcast(new RoundPhaseUpdated(evt.New.Phase, evt.Previous.Phase));
            }

            if (!evt.New.BombState.Equals(evt.Previous.BombState))
            {
                dispatcher.Broadcast(new BombStateUpdated(evt.New.BombState, evt.Previous.BombState));
            }

            if (!evt.New.WinningTeam.Equals(evt.Previous.WinningTeam))
            {
                int current_round = _map.Round;

                dispatcher.Broadcast(new TeamRoundVictory(current_round, evt.New.WinningTeam));
                if (evt.New.WinningTeam == Nodes.PlayerTeam.CT)
                {
                    dispatcher.Broadcast(new TeamRoundLoss(current_round, Nodes.PlayerTeam.T));
                }
                else if (evt.New.WinningTeam == Nodes.PlayerTeam.T)
                {
                    dispatcher.Broadcast(new TeamRoundLoss(current_round, Nodes.PlayerTeam.CT));
                }
            }
        }
    }
}
