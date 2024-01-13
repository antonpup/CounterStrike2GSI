using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Enum list for each weapon type.
    /// </summary>
    public enum WeaponType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// The weapon is a knife.
        /// </summary>
        Knife,

        /// <summary>
        /// The weapon is a pistol.
        /// </summary>
        Pistol,

        /// <summary>
        /// The weapon is a submachine gun.
        /// </summary>
        SubmachineGun,

        /// <summary>
        /// The weapon is a rifle.
        /// </summary>
        Rifle,

        /// <summary>
        /// The weapon is a shotgun.
        /// </summary>
        Shotgun,

        /// <summary>
        /// The weapon is a machine gun.
        /// </summary>
        MachineGun,

        /// <summary>
        /// The weapon is a grenade.
        /// </summary>
        Grenade,

        /// <summary>
        /// The weapon is a sniper rifle.
        /// </summary>
        SniperRifle,

        /// <summary>
        /// The weapon is a C4 explosive.
        /// </summary>
        C4,

        /// <summary>
        /// The weapon is a coop-item.
        /// </summary>
        StackableItem,

        /// <summary>
        /// The weapon is a Danger Zone tablet.
        /// </summary>
        Tablet,

        /// <summary>
        /// The weapon is Danger Zone fists.
        /// </summary>
        Fists,

        /// <summary>
        /// The weapon is a Danger Zone breach charge.
        /// </summary>
        BreachCharge,

        /// <summary>
        /// The weapon is a Danger Zone melee weapon.
        /// </summary>
        Melee
    }

    /// <summary>
    /// Enum list for each weapon state.
    /// </summary>
    public enum WeaponState
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// The weapon is a holstered.
        /// </summary>
        Holstered,

        /// <summary>
        /// The weapon is in use.
        /// </summary>
        Active,

        /// <summary>
        /// The weapon is being reloaded.
        /// </summary>
        Reloading
    }

    /// <summary>
    /// Information about the Weapon.
    /// </summary>
    public class Weapon : Node
    {
        /// <summary>
        /// The weapon name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The weapon skin name.
        /// </summary>
        public readonly string PaintKit;

        /// <summary>
        /// The weapon type.
        /// </summary>
        public readonly WeaponType Type;

        /// <summary>
        /// The amount of ammo in the weapon clip.
        /// </summary>
        public readonly int AmmoClip;

        /// <summary>
        /// The maximum amount of ammo in the weapon clip.
        /// </summary>
        public readonly int AmmoClipMax;

        /// <summary>
        /// The amount of ammo for the weapon in reserves.
        /// </summary>
        public readonly int AmmoReserve;

        /// <summary>
        /// The weapon state.
        /// </summary>
        public readonly WeaponState State;

        internal Weapon(JObject parsed_data = null) : base(parsed_data)
        {
            Name = GetString("name");
            PaintKit = GetString("paintkit");
            Type = GetEnum<WeaponType>("type");
            AmmoClip = GetInt("ammo_clip");
            AmmoClipMax = GetInt("ammo_clip_max");
            AmmoReserve = GetInt("ammo_reserve");
            State = GetEnum<WeaponState>("state");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Name: {Name}, " +
                $"PaintKit: {PaintKit}, " +
                $"Type: {Type}, " +
                $"AmmoClip: {AmmoClip}, " +
                $"AmmoClipMax: {AmmoClipMax}, " +
                $"AmmoReserve: {AmmoReserve}, " +
                $"State: {State}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Weapon other &&
                Name.Equals(other.Name) &&
                PaintKit.Equals(other.PaintKit) &&
                Type.Equals(other.Type) &&
                AmmoClip.Equals(other.AmmoClip) &&
                AmmoClipMax.Equals(other.AmmoClipMax) &&
                AmmoReserve.Equals(other.AmmoReserve) &&
                State.Equals(other.State);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 896140043;
            hashCode = hashCode * -659784304 + Name.GetHashCode();
            hashCode = hashCode * -659784304 + PaintKit.GetHashCode();
            hashCode = hashCode * -659784304 + Type.GetHashCode();
            hashCode = hashCode * -659784304 + AmmoClip.GetHashCode();
            hashCode = hashCode * -659784304 + AmmoClipMax.GetHashCode();
            hashCode = hashCode * -659784304 + AmmoReserve.GetHashCode();
            hashCode = hashCode * -659784304 + State.GetHashCode();
            return hashCode;
        }
    }
}
