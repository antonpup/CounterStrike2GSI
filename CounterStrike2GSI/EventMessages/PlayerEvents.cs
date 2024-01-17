using CounterStrike2GSI.Nodes;
using System.Collections.Generic;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Player update. 
    /// </summary>
    public class PlayerUpdated : UpdateEvent<Player>
    {
        /// <summary>
        /// The player ID of the updated player.
        /// </summary>
        public readonly string PlayerID;
        public PlayerUpdated(Player new_value, Player previous_value, string player_id) : base(new_value, previous_value)
        {
            PlayerID = player_id;
        }
    }

    /// <summary>
    /// Event for specific player's team change.
    /// </summary>
    public class PlayerTeamChanged : PlayerUpdateEvent<PlayerTeam>
    {
        public PlayerTeamChanged(PlayerTeam new_value, PlayerTeam previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's activity state update.
    /// </summary>
    public class PlayerActivityChanged : PlayerUpdateEvent<PlayerActivity>
    {
        public PlayerActivityChanged(PlayerActivity new_value, PlayerActivity previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's state update.
    /// </summary>
    public class PlayerStateChanged : PlayerUpdateEvent<PlayerState>
    {
        public PlayerStateChanged(PlayerState new_value, PlayerState previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's health update.
    /// </summary>
    public class PlayerHealthChanged : PlayerUpdateEvent<int>
    {
        public PlayerHealthChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's death.
    /// </summary>
    public class PlayerDied : PlayerHealthChanged
    {
        public PlayerDied(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's respawn.
    /// </summary>
    public class PlayerRespawned : PlayerHealthChanged
    {
        public PlayerRespawned(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's taking damage.
    /// </summary>
    public class PlayerTookDamage : PlayerHealthChanged
    {
        public PlayerTookDamage(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's armor update.
    /// </summary>
    public class PlayerArmorChanged : PlayerUpdateEvent<int>
    {
        public PlayerArmorChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's helmet update.
    /// </summary>
    public class PlayerHelmetChanged : PlayerUpdateEvent<bool>
    {
        public PlayerHelmetChanged(bool new_value, bool previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's flash amount update.
    /// </summary>
    public class PlayerFlashAmountChanged : PlayerUpdateEvent<int>
    {
        public PlayerFlashAmountChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's smoked amount update.
    /// </summary>
    public class PlayerSmokedAmountChanged : PlayerUpdateEvent<int>
    {
        public PlayerSmokedAmountChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's burning amount update.
    /// </summary>
    public class PlayerBurningAmountChanged : PlayerUpdateEvent<int>
    {
        public PlayerBurningAmountChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's money amount update.
    /// </summary>
    public class PlayerMoneyAmountChanged : PlayerUpdateEvent<int>
    {
        public PlayerMoneyAmountChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's round kills update.
    /// </summary>
    public class PlayerRoundKillsChanged : PlayerUpdateEvent<int>
    {
        public PlayerRoundKillsChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's round headshot kills update.
    /// </summary>
    public class PlayerRoundHeadshotKillsChanged : PlayerUpdateEvent<int>
    {
        public PlayerRoundHeadshotKillsChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's round total damage update.
    /// </summary>
    public class PlayerRoundTotalDamageChanged : PlayerUpdateEvent<int>
    {
        public PlayerRoundTotalDamageChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's equipment value update.
    /// </summary>
    public class PlayerEquipmentValueChanged : PlayerUpdateEvent<int>
    {
        public PlayerEquipmentValueChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's defuse kit update.
    /// </summary>
    public class PlayerDefusekitChanged : PlayerUpdateEvent<bool>
    {
        public PlayerDefusekitChanged(bool new_value, bool previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's weapon update.
    /// </summary>
    public class PlayerWeaponChanged : PlayerUpdateEvent<Weapon>
    {
        public PlayerWeaponChanged(Weapon new_value, Weapon previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's active weapon update.
    /// </summary>
    public class PlayerActiveWeaponChanged : PlayerWeaponChanged
    {
        public PlayerActiveWeaponChanged(Weapon new_value, Weapon previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player picking up weapons.
    /// </summary>
    public class PlayerWeaponsPickedUp : PlayerEvent
    {
        /// <summary>
        /// The picked up weapons.
        /// </summary>
        public readonly List<Weapon> Weapons;

        public PlayerWeaponsPickedUp(List<Weapon> weapons, Player player) : base(player)
        {
            Weapons = weapons;
        }
    }

    /// <summary>
    /// Event for specific player dropping weapons.
    /// </summary>
    public class PlayerWeaponsDropped : PlayerEvent
    {
        /// <summary>
        /// The dropped weapons.
        /// </summary>
        public readonly List<Weapon> Weapons;

        public PlayerWeaponsDropped(List<Weapon> weapons, Player player) : base(player)
        {
            Weapons = weapons;
        }
    }

    /// <summary>
    /// Event for specific player's statistics update.
    /// </summary>
    public class PlayerStatsChanged : PlayerUpdateEvent<MatchStats>
    {
        public PlayerStatsChanged(MatchStats new_value, MatchStats previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Kills statistic update.
    /// </summary>
    public class PlayerKillsChanged : PlayerUpdateEvent<int>
    {
        public PlayerKillsChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player earning a kill.
    /// </summary>
    public class PlayerGotKill : PlayerEvent
    {
        /// <summary>
        /// Was the kill a headshot?
        /// </summary>
        public readonly bool IsHeadshot;

        /// <summary>
        /// The weapon used to earn the kill.
        /// </summary>
        public readonly Weapon Weapon;

        /// <summary>
        /// Was the kill an ace kill?
        /// </summary>
        public readonly bool IsAce;

        public PlayerGotKill(bool is_headshot, Weapon weapon, bool is_ace, Player player) : base(player)
        {
            IsHeadshot = is_headshot;
            Weapon = weapon;
            IsAce = is_ace;
        }
    }

    /// <summary>
    /// Event for specific player's Assists statistic update.
    /// </summary>
    public class PlayerAssistsChanged : PlayerUpdateEvent<int>
    {
        public PlayerAssistsChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Deaths statistic update.
    /// </summary>
    public class PlayerDeathsChanged : PlayerUpdateEvent<int>
    {
        public PlayerDeathsChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's MVPs statistic update.
    /// </summary>
    public class PlayerMVPsChanged : PlayerUpdateEvent<int>
    {
        public PlayerMVPsChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Score statistic update.
    /// </summary>
    public class PlayerScoreChanged : PlayerUpdateEvent<int>
    {
        public PlayerScoreChanged(int new_value, int previous_value, Player player) : base(new_value, previous_value, player)
        {
        }
    }
}
