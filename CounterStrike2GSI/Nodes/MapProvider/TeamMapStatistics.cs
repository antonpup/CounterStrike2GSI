using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Class representing team statistics.
    /// </summary>
    public class TeamMapStatistics : Node
    {
        /// <summary>
        /// The team score.
        /// </summary>
        public readonly int Score;

        /// <summary>
        /// The team name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The team flag.
        /// </summary>
        public readonly string Flag;

        /// <summary>
        /// The consecutive rounds lost by the team.
        /// </summary>
        public readonly int ConsecutiveRoundLosses;

        /// <summary>
        /// The number of remaining timeouts.
        /// </summary>
        public readonly int RemainingTimeouts;

        /// <summary>
        /// The number of matches won in this series.
        /// </summary>
        public readonly int MatchesWonThisSeries;

        internal TeamMapStatistics(JObject parsed_data = null) : base(parsed_data)
        {
            Score = GetInt("score");
            Name = GetString("name");
            Flag = GetString("flag");
            ConsecutiveRoundLosses = GetInt("consecutive_round_losses");
            RemainingTimeouts = GetInt("timeouts_remaining");
            MatchesWonThisSeries = GetInt("timeouts_remaining");
        }

        public override string ToString()
        {
            return $"[" +
                $"Score: {Score}, " +
                $"Name: {Name}, " +
                $"Flag: {Flag}, " +
                $"ConsecutiveRoundLosses: {ConsecutiveRoundLosses}" +
                $"RemainingTimeouts: {RemainingTimeouts}" +
                $"MatchesWonThisSeries: {MatchesWonThisSeries}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is TeamMapStatistics other &&
                Score.Equals(other.Score) &&
                Name.Equals(other.Name) &&
                Flag.Equals(other.Flag) &&
                ConsecutiveRoundLosses.Equals(other.ConsecutiveRoundLosses) &&
                RemainingTimeouts.Equals(other.RemainingTimeouts) &&
                MatchesWonThisSeries.Equals(other.MatchesWonThisSeries);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 354721465;
            hashCode = hashCode * -365789412 + Score.GetHashCode();
            hashCode = hashCode * -365789412 + Name.GetHashCode();
            hashCode = hashCode * -365789412 + Flag.GetHashCode();
            hashCode = hashCode * -365789412 + ConsecutiveRoundLosses.GetHashCode();
            hashCode = hashCode * -365789412 + RemainingTimeouts.GetHashCode();
            hashCode = hashCode * -365789412 + MatchesWonThisSeries.GetHashCode();
            return hashCode;
        }
    }
}
