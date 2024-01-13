using CounterStrike2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Enum list for each grenade type.
    /// </summary>
    public enum GrenadeType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Smoke grenade.
        /// </summary>
        Smoke,

        /// <summary>
        /// Decoy grenade.
        /// </summary>
        Decoy,

        /// <summary>
        /// Fire grenade.
        /// </summary>
        Firebomb,

        /// <summary>
        /// Molotov grenade.
        /// </summary>
        Inferno,

        /// <summary>
        /// Flashbang grenade.
        /// </summary>
        Glashbang,

        /// <summary>
        /// Fragmentation grenade.
        /// </summary>
        Frag
    }

    /// <summary>
    /// Information about a Grenade.
    /// </summary>
    public class Grenade : Node
    {
        /// <summary>
        /// The owner of the grenade.
        /// </summary>
        public readonly string Owner;

        /// <summary>
        /// The grenade position.
        /// </summary>
        public readonly Vector3D Position;

        /// <summary>
        /// The grenade velocity.
        /// </summary>
        public readonly Vector3D Velocity;

        /// <summary>
        /// The grenade lifetime (in seconds).
        /// </summary>
        public readonly float Lifetime;

        /// <summary>
        /// The grenade type.
        /// </summary>
        public readonly GrenadeType Type;

        /// <summary>
        /// The flame locations.
        /// </summary>
        public readonly NodeMap<string, Vector3D> Flames = new NodeMap<string, Vector3D>();

        /// <summary>
        /// The grenade's effect time (in seconds).
        /// </summary>
        public readonly float EffectTime;

        private Regex _flame_regex = new Regex(@"flame_.+");

        internal Grenade(JObject parsed_data = null) : base(parsed_data)
        {
            Owner = GetString("owner");
            Position = new Vector3D(GetString("position"));
            Velocity = new Vector3D(GetString("velocity"));
            Lifetime = GetFloat("lifetime");
            Type = GetEnum<GrenadeType>("type");

            GetMatchingStrings(GetJObject("flames"), _flame_regex, (Match match, string str) =>
            {
                var flame_id = match.Groups[0].Value;
                var location = new Vector3D(str);

                if (!Flames.ContainsKey(flame_id))
                {
                    Flames.Add(flame_id, location);
                }
                else
                {
                    Flames[flame_id] = location;
                }
            });

            EffectTime = GetFloat("effecttime");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Owner: {Owner}, " +
                $"Position: {Position}, " +
                $"Velocity: {Velocity}, " +
                $"Lifetime: {Lifetime}, " +
                $"Type: {Type}, " +
                $"Flames: {Flames}, " +
                $"EffectTime: {EffectTime}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Grenade other &&
                Owner.Equals(other.Owner) &&
                Position.Equals(other.Position) &&
                Velocity.Equals(other.Velocity) &&
                Lifetime.Equals(other.Lifetime) &&
                Type.Equals(other.Type) &&
                Flames.Equals(other.Flames) &&
                EffectTime.Equals(other.EffectTime);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 120353541;
            hashCode = hashCode * -849620394 + Owner.GetHashCode();
            hashCode = hashCode * -849620394 + Position.GetHashCode();
            hashCode = hashCode * -849620394 + Velocity.GetHashCode();
            hashCode = hashCode * -849620394 + Lifetime.GetHashCode();
            hashCode = hashCode * -849620394 + Type.GetHashCode();
            hashCode = hashCode * -849620394 + Flames.GetHashCode();
            hashCode = hashCode * -849620394 + EffectTime.GetHashCode();
            return hashCode;
        }
    }
}
