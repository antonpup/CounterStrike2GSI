using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Information about the Phase Countdown.
    /// </summary>
    public class PhaseCountdowns : Node
    {
        /// <summary>
        /// The current phase.
        /// </summary>
        public readonly Phase Phase;

        /// <summary>
        /// The amount of time (in seconds) until Phase end.
        /// </summary>
        public readonly float PhaseEndTime;

        internal PhaseCountdowns(JObject parsed_data = null) : base(parsed_data)
        {
            Phase = GetEnum<Phase>("phase");
            PhaseEndTime = GetFloat("phase_ends_in");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Phase: {Phase}, " +
                $"PhaseEndTime: {PhaseEndTime}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is PhaseCountdowns other &&
                Phase.Equals(other.Phase) &&
                PhaseEndTime.Equals(other.PhaseEndTime);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 487516933;
            hashCode = hashCode * -210468954 + Phase.GetHashCode();
            hashCode = hashCode * -210468954 + PhaseEndTime.GetHashCode();
            return hashCode;
        }
    }
}
