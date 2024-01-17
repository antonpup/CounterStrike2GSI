using CounterStrike2GSI.EventMessages;
using CounterStrike2GSI.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace CounterStrike2GSI
{
    public class PlayerHandler : EventHandler<CS2GameEvent>
    {
        public PlayerHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<PlayerUpdated>(OnPlayerUpdated);
            dispatcher.Subscribe<PlayerStateChanged>(OnPlayerStateChanged);
            dispatcher.Subscribe<PlayerStatsChanged>(OnPlayerStatsChanged);
        }

        ~PlayerHandler()
        {
            dispatcher.Unsubscribe<PlayerUpdated>(OnPlayerUpdated);
            dispatcher.Unsubscribe<PlayerStateChanged>(OnPlayerStateChanged);
            dispatcher.Unsubscribe<PlayerStatsChanged>(OnPlayerStatsChanged);
        }

        private void OnPlayerUpdated(CS2GameEvent e)
        {
            PlayerUpdated evt = (e as PlayerUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.SteamID.Equals(evt.Previous.SteamID))
            {
                // Switched spectating to another player, no need for updates.
                return;
            }

            if (!evt.New.Team.Equals(evt.Previous.Team))
            {
                dispatcher.Broadcast(new PlayerTeamChanged(evt.New.Team, evt.Previous.Team, evt.New));
            }

            if (!evt.New.Activity.Equals(evt.Previous.Activity))
            {
                dispatcher.Broadcast(new PlayerActivityChanged(evt.New.Activity, evt.Previous.Activity, evt.New));
            }

            if (!evt.New.State.Equals(evt.Previous.State))
            {
                dispatcher.Broadcast(new PlayerStateChanged(evt.New.State, evt.Previous.State, evt.New));
            }

            if (!evt.New.Weapons.Equals(evt.Previous.Weapons))
            {
                List<Weapon> new_weapons = new List<Weapon>();
                List<Weapon> lost_weapons = new List<Weapon>();

                foreach (var weapon in evt.New.Weapons)
                {
                    var previous_weapon = evt.Previous.Weapons.FirstOrDefault(value => value.Name == weapon.Name, new Weapon());
                    if (!previous_weapon.IsValid())
                    {
                        // The player did not previously have the weapon.
                        new_weapons.Add(weapon);
                    }
                    else
                    {
                        if (!weapon.Name.Equals(previous_weapon.Name))
                        {
                            dispatcher.Broadcast(new PlayerWeaponChanged(weapon, previous_weapon, evt.New));
                        }
                    }
                }

                foreach (var weapon in evt.Previous.Weapons)
                {
                    var new_weapon = evt.New.Weapons.FirstOrDefault(value => value.Name == weapon.Name, new Weapon());
                    if (!new_weapon.IsValid())
                    {
                        // The player no longer has the weapon.
                        lost_weapons.Add(weapon);
                    }
                }

                if (new_weapons.Count > 0)
                {
                    dispatcher.Broadcast(new PlayerWeaponsPickedUp(new_weapons, evt.New));
                }

                if (lost_weapons.Count > 0)
                {
                    dispatcher.Broadcast(new PlayerWeaponsDropped(lost_weapons, evt.New));
                }

                var active_weapon = evt.New.GetActiveWeapon();
                var previous_active_weapon = evt.Previous.GetActiveWeapon();

                if (!active_weapon.Name.Equals(previous_active_weapon.Name))
                {
                    dispatcher.Broadcast(new PlayerActiveWeaponChanged(active_weapon, previous_active_weapon, evt.New));
                }
            }

            if (!evt.New.MatchStats.Equals(evt.Previous.MatchStats))
            {
                dispatcher.Broadcast(new PlayerStatsChanged(evt.New.MatchStats, evt.Previous.MatchStats, evt.New));
            }

            if ((evt.New.State.RoundKills > evt.Previous.State.RoundKills) && (evt.Previous.State.RoundKills != -1))
            {
                bool got_a_headshot = (evt.New.State.RoundHSKills > evt.Previous.State.RoundHSKills);
                var active_weapon = evt.New.GetActiveWeapon();
                if (!active_weapon.IsValid())
                {
                    active_weapon = evt.Previous.GetActiveWeapon();
                }
                dispatcher.Broadcast(new PlayerGotKill(got_a_headshot, active_weapon, evt.New.State.RoundKills >= 5, evt.New));
            }
        }

        private void OnPlayerStateChanged(CS2GameEvent e)
        {
            PlayerStateChanged evt = (e as PlayerStateChanged);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.Health.Equals(evt.Previous.Health))
            {
                dispatcher.Broadcast(new PlayerHealthChanged(evt.New.Health, evt.Previous.Health, evt.Player));

                if (evt.Previous.Health > evt.New.Health)
                {
                    dispatcher.Broadcast(new PlayerTookDamage(evt.New.Health, evt.Previous.Health, evt.Player));
                }

                if (evt.New.Health == 0)
                {
                    dispatcher.Broadcast(new PlayerDied(evt.New.Health, evt.Previous.Health, evt.Player));
                }
                else if (evt.New.Health > 0 && evt.Previous.Health == 0)
                {
                    dispatcher.Broadcast(new PlayerRespawned(evt.New.Health, evt.Previous.Health, evt.Player));
                }
            }

            if (!evt.New.Armor.Equals(evt.Previous.Armor))
            {
                dispatcher.Broadcast(new PlayerArmorChanged(evt.New.Armor, evt.Previous.Armor, evt.Player));
            }

            if (!evt.New.HasHelmet.Equals(evt.Previous.HasHelmet))
            {
                dispatcher.Broadcast(new PlayerHelmetChanged(evt.New.HasHelmet, evt.Previous.HasHelmet, evt.Player));
            }

            if (!evt.New.FlashAmount.Equals(evt.Previous.FlashAmount))
            {
                dispatcher.Broadcast(new PlayerFlashAmountChanged(evt.New.FlashAmount, evt.Previous.FlashAmount, evt.Player));
            }

            if (!evt.New.SmokedAmount.Equals(evt.Previous.SmokedAmount))
            {
                dispatcher.Broadcast(new PlayerSmokedAmountChanged(evt.New.SmokedAmount, evt.Previous.SmokedAmount, evt.Player));
            }

            if (!evt.New.BurningAmount.Equals(evt.Previous.BurningAmount))
            {
                dispatcher.Broadcast(new PlayerBurningAmountChanged(evt.New.BurningAmount, evt.Previous.BurningAmount, evt.Player));
            }

            if (!evt.New.Money.Equals(evt.Previous.Money))
            {
                dispatcher.Broadcast(new PlayerMoneyAmountChanged(evt.New.Money, evt.Previous.Money, evt.Player));
            }

            if (!evt.New.RoundKills.Equals(evt.Previous.RoundKills))
            {
                dispatcher.Broadcast(new PlayerRoundKillsChanged(evt.New.RoundKills, evt.Previous.RoundKills, evt.Player));
            }

            if (!evt.New.RoundHSKills.Equals(evt.Previous.RoundHSKills))
            {
                dispatcher.Broadcast(new PlayerRoundHeadshotKillsChanged(evt.New.RoundHSKills, evt.Previous.RoundHSKills, evt.Player));
            }

            if (!evt.New.RoundTotalDamage.Equals(evt.Previous.RoundTotalDamage))
            {
                dispatcher.Broadcast(new PlayerRoundTotalDamageChanged(evt.New.RoundTotalDamage, evt.Previous.RoundTotalDamage, evt.Player));
            }

            if (!evt.New.EquipmentValue.Equals(evt.Previous.EquipmentValue))
            {
                dispatcher.Broadcast(new PlayerEquipmentValueChanged(evt.New.EquipmentValue, evt.Previous.EquipmentValue, evt.Player));
            }

            if (!evt.New.HasDefuseKit.Equals(evt.Previous.HasDefuseKit))
            {
                dispatcher.Broadcast(new PlayerDefusekitChanged(evt.New.HasDefuseKit, evt.Previous.HasDefuseKit, evt.Player));
            }
        }

        private void OnPlayerStatsChanged(CS2GameEvent e)
        {
            PlayerStatsChanged evt = (e as PlayerStatsChanged);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.Kills.Equals(evt.Previous.Kills))
            {
                dispatcher.Broadcast(new PlayerKillsChanged(evt.New.Kills, evt.Previous.Kills, evt.Player));
            }

            if (!evt.New.Assists.Equals(evt.Previous.Assists))
            {
                dispatcher.Broadcast(new PlayerAssistsChanged(evt.New.Assists, evt.Previous.Assists, evt.Player));
            }

            if (!evt.New.Deaths.Equals(evt.Previous.Deaths))
            {
                dispatcher.Broadcast(new PlayerDeathsChanged(evt.New.Deaths, evt.Previous.Deaths, evt.Player));
            }

            if (!evt.New.MVPs.Equals(evt.Previous.MVPs))
            {
                dispatcher.Broadcast(new PlayerMVPsChanged(evt.New.MVPs, evt.Previous.MVPs, evt.Player));
            }

            if (!evt.New.Score.Equals(evt.Previous.Score))
            {
                dispatcher.Broadcast(new PlayerScoreChanged(evt.New.Score, evt.Previous.Score, evt.Player));
            }
        }
    }
}
