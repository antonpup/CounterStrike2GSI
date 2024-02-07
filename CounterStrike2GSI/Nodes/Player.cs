using CounterStrike2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Enum list for each player activity.
    /// </summary>
    public enum PlayerActivity
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// The player is playing.
        /// </summary>
        Playing,

        /// <summary>
        /// The player is in a menu.
        /// </summary>
        Menu,

        /// <summary>
        /// The player is inputting text.
        /// </summary>
        TextInput
    }

    /// <summary>
    /// Information about the Player.
    /// </summary>
    public class Player : Node
    {
        /// <summary>
        /// The player's Steam ID.
        /// </summary>
        public readonly string SteamID;

        /// <summary>
        /// The player's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The player's XP Overload level.
        /// </summary>
        public readonly int XPOverloadLevel;

        /// <summary>
        /// The player's clan.
        /// </summary>
        public readonly string Clan;

        /// <summary>
        /// The current observed player.
        /// </summary>
        public readonly int ObserverSlot;

        /// <summary>
        /// The player's team.
        /// </summary>
        public readonly PlayerTeam Team;

        /// <summary>
        /// The player's activity.
        /// </summary>
        public readonly PlayerActivity Activity;

        /// <summary>
        /// The player's current state.
        /// </summary>
        public readonly PlayerState State;

        /// <summary>
        /// The player's weapons.
        /// </summary>
        public readonly NodeList<Weapon> Weapons = new NodeList<Weapon>();

        /// <summary>
        /// The player's match statistics.
        /// </summary>
        public readonly MatchStats MatchStats;

        /// <summary>
        /// The player's spectation target. (SPECTATOR ONLY)
        /// </summary>
        public readonly string SpectationTarget;

        /// <summary>
        /// The player's position. (SPECTATOR ONLY)
        /// </summary>
        public readonly Vector3D Position;

        /// <summary>
        /// The player's forward direction. (SPECTATOR ONLY)
        /// </summary>
        public readonly Vector3D ForwardDirection;

        private Regex _weapon_regex = new Regex(@"weapon_(\d+)");

        internal Player(JObject parsed_data = null, string steam_id = "") : base(parsed_data)
        {
            SteamID = GetString("steamid");

            if (string.IsNullOrWhiteSpace(SteamID))
            {
                SteamID = steam_id;
            }

            Clan = GetString("clan");
            Name = GetString("name");
            XPOverloadLevel = GetInt("xpoverload");
            ObserverSlot = GetInt("observer_slot");
            Team = GetEnum<PlayerTeam>("team");
            Activity = GetEnum<PlayerActivity>("activity");
            State = new PlayerState(GetJObject("state"));

            GetMatchingObjects(GetJObject("weapons"), _weapon_regex, (Match match, JObject obj) =>
            {
                var weapon_index = Convert.ToInt32(match.Groups[1].Value);
                var weapon = new Weapon(obj);

                Weapons.Add(weapon);
            });

            MatchStats = new MatchStats(GetJObject("match_stats"));
            SpectationTarget = GetString("spectarget");
            Position = new Vector3D(GetString("position"));
            ForwardDirection = new Vector3D(GetString("forward"));
        }


        /// <summary>
        /// Gets the active weapon.
        /// </summary>
        /// <returns>The active weapon.</returns>
        public Weapon GetActiveWeapon()
        {
            foreach (var weapon in Weapons)
            {
                if (weapon.State == WeaponState.Active || weapon.State == WeaponState.Reloading)
                {
                    return weapon;
                }
            }

            // No active weapon.
            return new Weapon();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"SteamID: {SteamID}, " +
                $"Clan: {Clan}, " +
                $"Name: {Name}, " +
                $"ObserverSlot: {ObserverSlot}, " +
                $"Team: {Team}, " +
                $"Activity: {Activity}, " +
                $"State: {State}, " +
                $"Weapons: {Weapons}, " +
                $"MatchStats: {MatchStats}, " +
                $"SpectationTarget: {SpectationTarget}, " +
                $"Position: {Position}, " +
                $"ForwardDirection: {ForwardDirection}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Player other &&
                SteamID.Equals(other.SteamID) &&
                Clan.Equals(other.Clan) &&
                Name.Equals(other.Name) &&
                ObserverSlot.Equals(other.ObserverSlot) &&
                Activity.Equals(other.Activity) &&
                State.Equals(other.State) &&
                Weapons.Equals(other.Weapons) &&
                MatchStats.Equals(other.MatchStats) &&
                SpectationTarget.Equals(other.SpectationTarget) &&
                Position.Equals(other.Position) &&
                ForwardDirection.Equals(other.ForwardDirection);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 654302810;
            hashCode = hashCode * -658414789 + SteamID.GetHashCode();
            hashCode = hashCode * -658414789 + Clan.GetHashCode();
            hashCode = hashCode * -658414789 + Name.GetHashCode();
            hashCode = hashCode * -658414789 + ObserverSlot.GetHashCode();
            hashCode = hashCode * -658414789 + Activity.GetHashCode();
            hashCode = hashCode * -658414789 + State.GetHashCode();
            hashCode = hashCode * -658414789 + Weapons.GetHashCode();
            hashCode = hashCode * -658414789 + MatchStats.GetHashCode();
            hashCode = hashCode * -658414789 + SpectationTarget.GetHashCode();
            hashCode = hashCode * -658414789 + Position.GetHashCode();
            hashCode = hashCode * -658414789 + ForwardDirection.GetHashCode();
            return hashCode;
        }
    }
}
