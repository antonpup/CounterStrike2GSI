using CounterStrike2GSI.EventMessages;

namespace CounterStrike2GSI
{
    public class AuthHandler : EventHandler<CS2GameEvent>
    {
        public AuthHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<AuthUpdated>(OnAuthUpdated);
        }

        ~AuthHandler()
        {
            dispatcher.Unsubscribe<AuthUpdated>(OnAuthUpdated);
        }

        private void OnAuthUpdated(CS2GameEvent e)
        {
            AuthUpdated evt = (e as AuthUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
