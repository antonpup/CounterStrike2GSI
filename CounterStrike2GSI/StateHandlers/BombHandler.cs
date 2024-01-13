using CounterStrike2GSI.EventMessages;

namespace CounterStrike2GSI
{
    public class BombHandler : EventHandler<CS2GameEvent>
    {
        public BombHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<BombUpdated>(OnBombUpdated);
        }

        ~BombHandler()
        {
            dispatcher.Unsubscribe<BombUpdated>(OnBombUpdated);
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
            }
        }
    }
}
