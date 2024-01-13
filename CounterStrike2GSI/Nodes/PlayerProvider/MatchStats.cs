using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Information about the Match Statistics.
    /// </summary>
    public class MatchStats : Node
    {
        /// <summary>
        /// The number of kills.
        /// </summary>
        public readonly int Kills;

        /// <summary>
        /// The number of assits.
        /// </summary>
        public readonly int Assists;

        /// <summary>
        /// The number of deaths.
        /// </summary>
        public readonly int Deaths;

        /// <summary>
        /// The number of MVPs.
        /// </summary>
        public readonly int MVPs;

        /// <summary>
        /// The amount of score.
        /// </summary>
        public readonly int Score;

        internal MatchStats(JObject parsed_data = null) : base(parsed_data)
        {
            Kills = GetInt("kills");
            Assists = GetInt("assists");
            Deaths = GetInt("deaths");
            MVPs = GetInt("mvps");
            Score = GetInt("score");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Kills: {Kills}, " +
                $"Assists: {Assists}, " +
                $"Deaths: {Deaths}, " +
                $"MVPs: {MVPs}, " +
                $"Score: {Score}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is MatchStats other &&
                Kills.Equals(other.Kills) &&
                Assists.Equals(other.Assists) &&
                Deaths.Equals(other.Deaths) &&
                MVPs.Equals(other.MVPs) &&
                Score.Equals(other.Score);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 986324056;
            hashCode = hashCode * -987302423 + Kills.GetHashCode();
            hashCode = hashCode * -987302423 + Assists.GetHashCode();
            hashCode = hashCode * -987302423 + Deaths.GetHashCode();
            hashCode = hashCode * -987302423 + MVPs.GetHashCode();
            hashCode = hashCode * -987302423 + Score.GetHashCode();
            return hashCode;
        }
    }
}
