using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Enum list for each player team.
    /// </summary>
    public enum PlayerTeam
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// The spectator team.
        /// </summary>
        Spectator,

        /// <summary>
        /// The Terrorist team.
        /// </summary>
        T,

        /// <summary>
        /// The Counter-Terrorist team.
        /// </summary>
        CT
    }

    /// <summary>
    /// Enum list for each phase.
    /// </summary>
    public enum Phase
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Freeze time.
        /// </summary>
        Freezetime,

        /// <summary>
        /// The round is undergoing.
        /// </summary>
        Live,

        /// <summary>
        /// The game is paused.
        /// </summary>
        Paused,

        /// <summary>
        /// The round is over.
        /// </summary>
        Over,

        /// <summary>
        /// The game is in intermission.
        /// </summary>
        Intermission,

        /// <summary>
        /// The game is over.
        /// </summary>
        Gameover,

        /// <summary>
        /// The round is over by bomb detonation.
        /// </summary>
        Bomb,

        /// <summary>
        /// The game is paused by Terrorist timeout.
        /// </summary>
        Timeout_T,

        /// <summary>
        /// The game is paused by Counter-Terrorist timeout.
        /// </summary>
        Timeout_CT,

        /// <summary>
        /// The round is over by bomb defusal.
        /// </summary>
        Defuse,

        /// <summary>
        /// The game is in warmup.
        /// </summary>
        Warmup
    }

    /// <summary>
    /// Information about the provider of this GameState.
    /// </summary>
    public class Round : Node
    {
        /// <summary>
        /// The current round phase.
        /// </summary>
        public readonly Phase Phase;

        /// <summary>
        /// The current bomb state.
        /// </summary>
        public readonly BombState BombState;

        /// <summary>
        /// The round winning team.
        /// </summary>
        public readonly PlayerTeam WinningTeam;

        internal Round(JObject parsed_data = null) : base(parsed_data)
        {
            Phase = GetEnum<Phase>("phase");
            BombState = GetEnum<BombState>("bomb");
            WinningTeam = GetEnum<PlayerTeam>("win_team");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Phase: {Phase}, " +
                $"BombState: {BombState}, " +
                $"WinningTeam: {WinningTeam}, " +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Round other &&
                Phase.Equals(other.Phase) &&
                BombState.Equals(other.BombState) &&
                WinningTeam.Equals(other.WinningTeam);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 659410546;
            hashCode = hashCode * -321047898 + Phase.GetHashCode();
            hashCode = hashCode * -321047898 + BombState.GetHashCode();
            hashCode = hashCode * -321047898 + WinningTeam.GetHashCode();
            return hashCode;
        }
    }
}
