using CounterStrike2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Enum list for each bomb activity.
    /// </summary>
    public enum BombState
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// The bomb is carried.
        /// </summary>
        Carried,

        /// <summary>
        /// The bomb is dropped.
        /// </summary>
        Dropped,

        /// <summary>
        /// The bomb is planted.
        /// </summary>
        Planted,

        /// <summary>
        /// The bomb is being planted.
        /// </summary>
        Planting,

        /// <summary>
        /// The bomb is being defused.
        /// </summary>
        Defusing,

        /// <summary>
        /// The bomb detonated.
        /// </summary>
        Exploded,

        /// <summary>
        /// The bomb has been defused.
        /// </summary>
        Defused
    }

    /// <summary>
    /// Information about the Bomb.
    /// </summary>
    public class Bomb : Node
    {
        /// <summary>
        /// The current bomb state.
        /// </summary>
        public readonly BombState State;

        /// <summary>
        /// The current bomb position.
        /// </summary>
        public readonly Vector3D Position;

        /// <summary>
        /// The current player in possession of the bomb.
        /// </summary>
        public readonly string Player;

        /// <summary>
        /// The current bomb countdown.
        /// </summary>
        public readonly float Countdown;

        internal Bomb(JObject parsed_data = null) : base(parsed_data)
        {
            State = GetEnum<BombState>("state");
            Position = new Vector3D(GetString("position"));
            Countdown = GetFloat("countdown");
            Player = GetString("player");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"State: {State}, " +
                $"Position: {Position}, " +
                $"Countdown: {Countdown}, " +
                $"Player: {Player}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Bomb other &&
                State.Equals(other.State) &&
                Position.Equals(other.Position) &&
                Countdown.Equals(other.Countdown) &&
                Player.Equals(other.Player);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 561468550;
            hashCode = hashCode * -604837261 + State.GetHashCode();
            hashCode = hashCode * -604837261 + Position.GetHashCode();
            hashCode = hashCode * -604837261 + Countdown.GetHashCode();
            hashCode = hashCode * -604837261 + Player.GetHashCode();
            return hashCode;
        }
    }
}
