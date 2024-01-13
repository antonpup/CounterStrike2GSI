using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Enum list for each map mode.
    /// </summary>
    public enum GameMode
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Custom game mode.
        /// </summary>
        Custom,

        /// <summary>
        /// War games game mode.
        /// </summary>
        Skirmish,

        /// <summary>
        /// Competitive game mode.
        /// </summary>
        Competitive,

        /// <summary>
        /// Wingman game mode.
        /// </summary>
        Scrimcomp2v2,

        /// <summary>
        /// Weapons Expert game mode.
        /// </summary>
        Scrimcomp5v5,

        /// <summary>
        /// Casual game mode.
        /// </summary>
        Casual,

        /// <summary>
        /// Mission game mode.
        /// </summary>
        Cooperative,

        /// <summary>
        /// Training game mode.
        /// </summary>
        Training,

        /// <summary>
        /// Deathmatch game mode.
        /// </summary>
        Deathmatch
    }

    /// <summary>
    /// Enum list for each player team.
    /// </summary>
    public enum RoundConclusion
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Terrorists win by elimination.
        /// </summary>
        T_Win_Elimination,

        /// <summary>
        /// Terrorists win by bomb detonation.
        /// </summary>
        T_Win_Bomb,

        /// <summary>
        /// Terrorists win by time.
        /// </summary>
        T_Win_Time,

        /// <summary>
        /// Counter-Terrorists win by elimination.
        /// </summary>
        CT_Win_Elimination,

        /// <summary>
        /// Counter-Terrorists win by bomb defusion.
        /// </summary>
        CT_Win_Defuse,

        /// <summary>
        /// Counter-Terrorists win by rescuing a hostage.
        /// </summary>
        CT_Win_Rescue,

        /// <summary>
        /// Counter-Terrorists win by time.
        /// </summary>
        CT_Win_Time
    }

    /// <summary>
    /// Information about the Map.
    /// </summary>
    public class Map : Node
    {
        /// <summary>
        /// The game mode.
        /// </summary>
        public readonly GameMode Mode;

        /// <summary>
        /// The map name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The current map phase.
        /// </summary>
        public readonly Phase Phase;

        /// <summary>
        /// The current round.
        /// </summary>
        public readonly int Round;

        /// <summary>
        /// The Counter-Terrorist statistics.
        /// </summary>
        public readonly TeamMapStatistics CTStatistics;

        /// <summary>
        /// The Terrorist statistics.
        /// </summary>
        public readonly TeamMapStatistics TStatistics;

        /// <summary>
        /// The number of matches required to win the series.
        /// </summary>
        public readonly int NumberOfMatchesToWinSeries;

        /// <summary>
        /// The round conclusions. Key is round number, value is the round conclusion.
        /// </summary>
        public readonly NodeMap<int, RoundConclusion> RoundWins = new NodeMap<int, RoundConclusion>();

        private Regex _round_regex = new Regex(@"(\d+)");

        internal Map(JObject parsed_data = null) : base(parsed_data)
        {
            Mode = GetEnum<GameMode>("mode");
            Name = GetString("name");
            Phase = GetEnum<Phase>("phase");
            Round = GetInt("round");
            CTStatistics = new TeamMapStatistics(GetJObject("team_ct"));
            TStatistics = new TeamMapStatistics(GetJObject("team_t"));
            NumberOfMatchesToWinSeries = GetInt("num_matches_to_win_series");

            GetMatchingStrings(GetJObject("round_wins"), _round_regex, (Match match, string str) =>
            {
                var round = Convert.ToInt32(match.Groups[1].Value);
                var round_conclusion = ToEnum<RoundConclusion>(str);

                if (!RoundWins.ContainsKey(round))
                {
                    RoundWins.Add(round, round_conclusion);
                }
                else
                {
                    RoundWins[round] = round_conclusion;
                }
            });
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Mode: {Mode}, " +
                $"Name: {Name}, " +
                $"Phase: {Phase}, " +
                $"Round: {Round}" +
                $"CTStatistics: {CTStatistics}" +
                $"TStatistics: {TStatistics}" +
                $"NumberOfMatchesToWinSeries: {NumberOfMatchesToWinSeries}" +
                $"RoundWins: {RoundWins}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Map other &&
                Mode.Equals(other.Mode) &&
                Name.Equals(other.Name) &&
                Phase.Equals(other.Phase) &&
                Round.Equals(other.Round) &&
                CTStatistics.Equals(other.CTStatistics) &&
                TStatistics.Equals(other.TStatistics) &&
                NumberOfMatchesToWinSeries.Equals(other.NumberOfMatchesToWinSeries) &&
                RoundWins.Equals(other.RoundWins);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 897951664;
            hashCode = hashCode * -645891238 + Mode.GetHashCode();
            hashCode = hashCode * -645891238 + Name.GetHashCode();
            hashCode = hashCode * -645891238 + Phase.GetHashCode();
            hashCode = hashCode * -645891238 + Round.GetHashCode();
            hashCode = hashCode * -645891238 + CTStatistics.GetHashCode();
            hashCode = hashCode * -645891238 + TStatistics.GetHashCode();
            hashCode = hashCode * -645891238 + NumberOfMatchesToWinSeries.GetHashCode();
            hashCode = hashCode * -645891238 + RoundWins.GetHashCode();
            return hashCode;
        }
    }
}
