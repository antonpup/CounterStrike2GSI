using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Information about the provider of this GameState.
    /// </summary>
    public class Provider : Node
    {
        /// <summary>
        /// Game name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Game Steam AppID.
        /// </summary>
        public readonly int AppID;

        /// <summary>
        /// Game version.
        /// </summary>
        public readonly int Version;

        /// <summary>
        /// Local player's Steam ID.
        /// </summary>
        public readonly string SteamID;

        /// <summary>
        ///  Timestamp of the GameState data.
        /// </summary>
        public readonly int Timestamp;

        internal Provider(JObject parsed_data = null) : base(parsed_data)
        {
            Name = GetString("name");
            AppID = GetInt("appid");
            Version = GetInt("version");
            SteamID = GetString("steamid");
            Timestamp = GetInt("timestamp");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Name: {Name}, " +
                $"AppID: {AppID}, " +
                $"Version: {Version}, " +
                $"SteamID: {SteamID}" +
                $"Timestamp: {Timestamp}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Provider other &&
                Name.Equals(other.Name) &&
                AppID == other.AppID &&
                Version == other.Version &&
                SteamID.Equals(other.SteamID);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 854512546;
            hashCode = hashCode * -845579214 + Name.GetHashCode();
            hashCode = hashCode * -845579214 + AppID.GetHashCode();
            hashCode = hashCode * -845579214 + Version.GetHashCode();
            hashCode = hashCode * -845579214 + SteamID.GetHashCode();
            hashCode = hashCode * -845579214 + Timestamp.GetHashCode();
            return hashCode;
        }
    }
}
