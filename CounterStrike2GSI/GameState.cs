using CounterStrike2GSI.Nodes;
using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI
{
    /// <summary>
    /// A class representing various information pertaining to Game State Integration of Counter-Strike 2.
    /// </summary>
    public class GameState : Node
    {
        /// <summary>
        /// Information about GSI authentication.<br/>
        /// Enabled by including <code>"auth" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Auth Auth;

        /// <summary>
        /// Information about the provider of this GameState.<br/>
        /// Enabled by including <code>"provider" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Provider Provider;

        /// <summary>
        /// Information about the current map.<br/>
        /// Enabled by including <code>"map" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Map Map;

        /// <summary>
        /// Information about the current round.<br/>
        /// Enabled by including <code>"round" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Round Round;

        /// <summary>
        /// Information about the local player or team players when spectating.<br/>
        /// Enabled by including any of the following in the game state cfg file:
        /// <code>"player_id" "1"</code><br/>
        /// <code>"player_state" "1"</code><br/>
        /// <code>"player_match_stats" "1"</code><br/>
        /// <code>"player_weapons" "1"</code><br/>
        /// <code>"player_position" "1"</code><br/>
        /// </summary>
        public readonly Player Player;

        /// <summary>
        /// Information about the phase countdowns. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"phase_countdowns" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly PhaseCountdowns PhaseCountdowns;

        /// <summary>
        /// Information about the all players in the game. (SPECTATOR ONLY)<br/>
        /// Enabled by including any of the following in the game state cfg file:
        /// <code>"allplayers_id" "1"</code><br/>
        /// <code>"allplayers_state" "1"</code><br/>
        /// <code>"allplayers_match_stats" "1"</code><br/>
        /// <code>"allplayers_weapons" "1"</code><br/>
        /// <code>"allplayers_position" "1"</code><br/>
        /// </summary>
        public readonly AllPlayers AllPlayers;

        /// <summary>
        /// Information about the all grenades in the game. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"allgrenades" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly AllGrenades AllGrenades;

        /// <summary>
        /// Information about the bomb in the game. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"bomb" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Bomb Bomb;

        /// <summary>
        /// Information about the tournament draft in the game. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"tournamentdraft" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly TournamentDraft TournamentDraft;


        /// <summary>
        /// A previous GameState.
        /// </summary>
        public GameState Previously
        {
            get
            {
                if (_previous_game_state == null)
                {
                    _previous_game_state = new GameState(GetJObject("previously"));
                }

                return _previous_game_state;
            }
        }

        private GameState _previous_game_state;

        // Helpers

        /// <summary>
        /// Creates a GameState instance based on the given json data.
        /// </summary>
        /// <param name="parsed_data">The parsed json data.</param>
        public GameState(JObject parsed_data = null) : base(parsed_data)
        {
            Auth = new Auth(GetJObject("auth"));
            Provider = new Provider(GetJObject("provider"));
            Map = new Map(GetJObject("map"));
            Round = new Round(GetJObject("round"));
            Player = new Player(GetJObject("player"));
            PhaseCountdowns = new PhaseCountdowns(GetJObject("phase_countdowns"));
            AllPlayers = new AllPlayers(GetJObject("allplayers"));
            AllGrenades = new AllGrenades(GetJObject("grenades"));
            Bomb = new Bomb(GetJObject("bomb"));
        }
    }
}
