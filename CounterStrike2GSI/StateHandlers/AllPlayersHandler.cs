using CounterStrike2GSI.EventMessages;

namespace CounterStrike2GSI
{
    public class AllPlayersHandler : EventHandler<CS2GameEvent>
    {
        public AllPlayersHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<AllPlayersUpdated>(OnAllPlayersUpdated);
        }

        ~AllPlayersHandler()
        {
            dispatcher.Unsubscribe<AllPlayersUpdated>(OnAllPlayersUpdated);
        }

        private void OnAllPlayersUpdated(CS2GameEvent e)
        {
            AllPlayersUpdated evt = (e as AllPlayersUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var player_kvp in evt.New)
            {
                if (!evt.Previous.ContainsKey(player_kvp.Key))
                {
                    // Player did not exist before.
                    continue;
                }

                var previous_player = evt.Previous[player_kvp.Key];

                if (!player_kvp.Value.Equals(previous_player))
                {
                    dispatcher.Broadcast(new PlayerUpdated(player_kvp.Value, previous_player, player_kvp.Key));
                }
            }
        }
    }
}
