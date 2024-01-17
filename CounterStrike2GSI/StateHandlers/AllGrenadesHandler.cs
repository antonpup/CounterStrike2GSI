using CounterStrike2GSI.EventMessages;

namespace CounterStrike2GSI
{
    public class AllGrenadesHandler : EventHandler<CS2GameEvent>
    {
        public AllGrenadesHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<AllGrenadesUpdated>(OnAllGrenadesUpdated);
        }

        ~AllGrenadesHandler()
        {
            dispatcher.Unsubscribe<AllGrenadesUpdated>(OnAllGrenadesUpdated);
        }

        private void OnAllGrenadesUpdated(CS2GameEvent e)
        {
            AllGrenadesUpdated evt = (e as AllGrenadesUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var grenade_kvp in evt.New)
            {
                if (!evt.Previous.ContainsKey(grenade_kvp.Key))
                {
                    // Grenade did not exist before.
                    dispatcher.Broadcast(new NewGrenade(grenade_kvp.Value, grenade_kvp.Key));
                    continue;
                }

                var previous_grenade = evt.Previous[grenade_kvp.Key];

                if (!grenade_kvp.Value.Equals(previous_grenade))
                {
                    dispatcher.Broadcast(new GrenadeUpdated(grenade_kvp.Value, previous_grenade, grenade_kvp.Key));
                }
            }

            foreach (var prev_grenade_kvp in evt.Previous)
            {
                if (!evt.New.ContainsKey(prev_grenade_kvp.Key))
                {
                    // Grenade does not exist anymore.
                    dispatcher.Broadcast(new ExpiredGrenade(prev_grenade_kvp.Value, prev_grenade_kvp.Key));
                    continue;
                }
            }
        }
    }
}
