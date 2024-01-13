using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Information about the Player State.
    /// </summary>
    public class PlayerState : Node
    {
        /// <summary>
        /// The player health amount.
        /// </summary>
        public readonly int Health;

        /// <summary>
        /// The player armor amount.
        /// </summary>
        public readonly int Armor;

        /// <summary>
        /// Does the player have a helmet?
        /// </summary>
        public readonly bool HasHelmet;

        /// <summary>
        /// The amount the player is flashed. From 0 to 255.
        /// </summary>
        public readonly int FlashAmount;

        /// <summary>
        /// The amount the player is smoked. From 0 to 255.
        /// </summary>
        public readonly int SmokedAmount;

        /// <summary>
        /// The amount the player is burning. From 0 to 255.
        /// </summary>
        public readonly int BurningAmount;

        /// <summary>
        /// The amount of money the player has.
        /// </summary>
        public readonly int Money;

        /// <summary>
        /// The number of kills the player has in the current round.
        /// </summary>
        public readonly int RoundKills;

        /// <summary>
        /// The number of headshot kills the player has in the current round.
        /// </summary>
        public readonly int RoundHSKills;

        /// <summary>
        /// The total damage amount the player has earned in the current round.
        /// </summary>
        public readonly int RoundTotalDamage;

        /// <summary>
        /// The total equipment value of the player.
        /// </summary>
        public readonly int EquipmentValue;

        /// <summary>
        /// Does the player have a defuse kit?
        /// </summary>
        public readonly bool HasDefuseKit;

        internal PlayerState(JObject parsed_data = null) : base(parsed_data)
        {
            Health = GetInt("health");
            Armor = GetInt("armor");
            HasHelmet = GetBool("helmet");
            HasDefuseKit = GetBool("defusekit");
            FlashAmount = GetInt("flashed");
            SmokedAmount = GetInt("smoked");
            BurningAmount = GetInt("burning");
            Money = GetInt("money");
            RoundKills = GetInt("round_kills");
            RoundHSKills = GetInt("round_killhs");
            RoundTotalDamage = GetInt("round_totaldmg");
            EquipmentValue = GetInt("equip_value");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Health: {Health}, " +
                $"Armor: {Armor}, " +
                $"HasHelmet: {HasHelmet}, " +
                $"HasDefuseKit: {HasDefuseKit}, " +
                $"FlashAmount: {FlashAmount}, " +
                $"SmokedAmount: {SmokedAmount}, " +
                $"BurningAmount: {BurningAmount}, " +
                $"Money: {Money}, " +
                $"RoundKills: {RoundKills}, " +
                $"RoundHSKills: {RoundHSKills}, " +
                $"RoundTotalDamage: {RoundTotalDamage}, " +
                $"EquipmentValue: {EquipmentValue}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is PlayerState other &&
                Health.Equals(other.Health) &&
                Armor.Equals(other.Armor) &&
                HasHelmet.Equals(other.HasHelmet) &&
                HasDefuseKit.Equals(other.HasDefuseKit) &&
                FlashAmount.Equals(other.FlashAmount) &&
                SmokedAmount.Equals(other.SmokedAmount) &&
                BurningAmount.Equals(other.BurningAmount) &&
                Money.Equals(other.Money) &&
                RoundKills.Equals(other.RoundKills) &&
                RoundHSKills.Equals(other.RoundHSKills) &&
                RoundTotalDamage.Equals(other.RoundTotalDamage) &&
                EquipmentValue.Equals(other.EquipmentValue);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 871364069;
            hashCode = hashCode * -398756301 + Health.GetHashCode();
            hashCode = hashCode * -398756301 + Armor.GetHashCode();
            hashCode = hashCode * -398756301 + HasHelmet.GetHashCode();
            hashCode = hashCode * -398756301 + HasDefuseKit.GetHashCode();
            hashCode = hashCode * -398756301 + FlashAmount.GetHashCode();
            hashCode = hashCode * -398756301 + SmokedAmount.GetHashCode();
            hashCode = hashCode * -398756301 + BurningAmount.GetHashCode();
            hashCode = hashCode * -398756301 + Money.GetHashCode();
            hashCode = hashCode * -398756301 + RoundKills.GetHashCode();
            hashCode = hashCode * -398756301 + RoundHSKills.GetHashCode();
            hashCode = hashCode * -398756301 + RoundTotalDamage.GetHashCode();
            hashCode = hashCode * -398756301 + EquipmentValue.GetHashCode();
            return hashCode;
        }
    }
}
