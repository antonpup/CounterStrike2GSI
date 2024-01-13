using CounterStrike2GSI.EventMessages;

namespace CounterStrike2GSI
{
    public class ProviderHandler : EventHandler<CS2GameEvent>
    {
        public ProviderHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<ProviderUpdated>(OnProviderUpdated);
        }

        ~ProviderHandler()
        {
            dispatcher.Unsubscribe<ProviderUpdated>(OnProviderUpdated);
        }

        private void OnProviderUpdated(CS2GameEvent e)
        {
            ProviderUpdated evt = (e as ProviderUpdated);

            if (evt == null)
            {
                return;
            }

        }
    }
}
