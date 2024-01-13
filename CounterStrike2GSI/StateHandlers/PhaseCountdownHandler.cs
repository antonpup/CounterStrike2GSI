using CounterStrike2GSI.EventMessages;

namespace CounterStrike2GSI
{
    public class PhaseCountdownsHandler : EventHandler<CS2GameEvent>
    {
        public PhaseCountdownsHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<PhaseCountdownsUpdated>(OnPhaseCountdownsUpdated);
        }

        ~PhaseCountdownsHandler()
        {
            dispatcher.Unsubscribe<PhaseCountdownsUpdated>(OnPhaseCountdownsUpdated);
        }

        private void OnPhaseCountdownsUpdated(CS2GameEvent e)
        {
            PhaseCountdownsUpdated evt = (e as PhaseCountdownsUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.Phase.Equals(evt.Previous.Phase))
            {
                dispatcher.Broadcast(new RoundPhaseUpdated(evt.New.Phase, evt.Previous.Phase));
            }
        }
    }
}
