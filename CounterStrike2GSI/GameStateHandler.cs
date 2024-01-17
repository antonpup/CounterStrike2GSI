using CounterStrike2GSI.EventMessages;

namespace CounterStrike2GSI
{
    public class GameStateHandler : EventHandler<CS2GameEvent>
    {
        private GameState previous_game_state = new GameState();

        public GameStateHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
        }

        public void OnNewGameState(GameState game_state)
        {
            if (!game_state.IsValid())
            {
                // Invalid game state provided, nothing to do here.
                return;
            }

            if (!previous_game_state.IsValid() && game_state.Previously.IsValid())
            {
                // If the previous game state cache is invlaid, attempt to recover it from the current game state.
                previous_game_state = game_state.Previously;
            }

            // Broadcast changes for providers.

            if (!previous_game_state.Auth.Equals(game_state.Auth))
            {
                dispatcher.Broadcast(new AuthUpdated(game_state.Auth, previous_game_state.Auth));
            }

            if (!previous_game_state.Provider.Equals(game_state.Provider))
            {
                dispatcher.Broadcast(new ProviderUpdated(game_state.Provider, previous_game_state.Provider));
            }

            if (!previous_game_state.Map.Equals(game_state.Map))
            {
                dispatcher.Broadcast(new MapUpdated(game_state.Map, previous_game_state.Map));
            }

            if (!previous_game_state.Round.Equals(game_state.Round))
            {
                // Depends on MapUpdated. This broadcast must happen after MapUpdated.
                dispatcher.Broadcast(new RoundUpdated(game_state.Round, previous_game_state.Round));
            }

            if (!previous_game_state.Player.Equals(game_state.Player))
            {
                dispatcher.Broadcast(new PlayerUpdated(game_state.Player, previous_game_state.Player, game_state.Player.SteamID));
            }

            if (!previous_game_state.PhaseCountdowns.Equals(game_state.PhaseCountdowns))
            {
                dispatcher.Broadcast(new PhaseCountdownsUpdated(game_state.PhaseCountdowns, previous_game_state.PhaseCountdowns));
            }

            if (!previous_game_state.AllPlayers.Equals(game_state.AllPlayers))
            {
                dispatcher.Broadcast(new AllPlayersUpdated(game_state.AllPlayers, previous_game_state.AllPlayers));
            }

            if (!previous_game_state.AllGrenades.Equals(game_state.AllGrenades))
            {
                dispatcher.Broadcast(new AllGrenadesUpdated(game_state.AllGrenades, previous_game_state.AllGrenades));
            }

            if (!previous_game_state.Bomb.Equals(game_state.Bomb))
            {
                // Depends on PlayerUpdated. This broadcast must happen after PlayerUpdated.
                dispatcher.Broadcast(new BombUpdated(game_state.Bomb, previous_game_state.Bomb));
            }

            // Finally update the previous game state cache.
            previous_game_state = game_state;
        }
    }
}
