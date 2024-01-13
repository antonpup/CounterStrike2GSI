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

            string player_id = string.IsNullOrWhiteSpace(evt.PlayerID) ? evt.New.SteamID : evt.PlayerID;

            if (!evt.New.Team.Equals(evt.Previous.Team))
            {
                dispatcher.Broadcast(new PlayerTeamChanged(evt.New.Team, evt.Previous.Team, player_id));
            }

            if (!evt.New.Activity.Equals(evt.Previous.Activity))
            {
                dispatcher.Broadcast(new PlayerActivityChanged(evt.New.Activity, evt.Previous.Activity, player_id));
            }

            if (!evt.New.State.Equals(evt.Previous.State))
            {
                dispatcher.Broadcast(new PlayerStateChanged(evt.New.State, evt.Previous.State, player_id));
            }

            if (!evt.New.Weapons.Equals(evt.Previous.Weapons))
            {
                List<Weapon> new_weapons = new List<Weapon>();

                foreach (var weapon_kvp in evt.New.Weapons)
                {
                    if (!evt.Previous.Weapons.ContainsKey(weapon_kvp.Key))
                    {
                        // The player did not previously have the weapon slot?
                        continue;
                    }

                    bool is_new_weapon = !evt.Previous.Weapons.Any(previous_weapon => (previous_weapon.Value.Name.Equals(weapon_kvp.Value.Name)));
                    if (is_new_weapon)
                    {
                        new_weapons.Add(weapon_kvp.Value);
                    }

                    var previous_weapon = evt.Previous.Weapons[weapon_kvp.Key];

                    if (!weapon_kvp.Value.Equals(previous_weapon))
                    {
                        dispatcher.Broadcast(new PlayerWeaponChanged(weapon_kvp.Value, previous_weapon, weapon_kvp.Key, player_id));
                    }
                }

                if (new_weapons.Count > 0)
                {
                    dispatcher.Broadcast(new PlayerWeaponsPickedUp(new_weapons, player_id));
                }

                var active_weapon = evt.New.GetActiveWeapon();
                var previous_active_weapon = evt.Previous.GetActiveWeapon();

                if (!active_weapon.Equals(previous_active_weapon))
                {
                    dispatcher.Broadcast(new PlayerActiveWeaponChanged(active_weapon, previous_active_weapon, evt.New.GetActiveWeaponSlot(), player_id));
                }
            }

            if (!evt.New.MatchStats.Equals(evt.Previous.MatchStats))
            {
                dispatcher.Broadcast(new PlayerStatsChanged(evt.New.MatchStats, evt.Previous.MatchStats, player_id));
            }

            if ((evt.New.State.RoundKills > evt.Previous.State.RoundKills) && (evt.Previous.State.RoundKills != -1))
            {
                bool got_a_headshot = (evt.New.State.RoundHSKills > evt.Previous.State.RoundHSKills);
                dispatcher.Broadcast(new PlayerGotKill(got_a_headshot, evt.New.GetActiveWeapon(), evt.New.State.RoundKills >= 5, evt.PlayerID));
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
                dispatcher.Broadcast(new PlayerHealthChanged(evt.New.Health, evt.Previous.Health, evt.PlayerID));

                if (evt.Previous.Health > evt.New.Health)
                {
                    dispatcher.Broadcast(new PlayerTookDamage(evt.New.Health, evt.Previous.Health, evt.PlayerID));
                }

                if (evt.New.Health == 0)
                {
                    dispatcher.Broadcast(new PlayerDied(evt.New.Health, evt.Previous.Health, evt.PlayerID));
                }
                else if (evt.New.Health > 0 && evt.Previous.Health == 0)
                {
                    dispatcher.Broadcast(new PlayerRespawned(evt.New.Health, evt.Previous.Health, evt.PlayerID));
                }
            }

            if (!evt.New.Armor.Equals(evt.Previous.Armor))
            {
                dispatcher.Broadcast(new PlayerArmorChanged(evt.New.Armor, evt.Previous.Armor, evt.PlayerID));
            }

            if (!evt.New.HasHelmet.Equals(evt.Previous.HasHelmet))
            {
                dispatcher.Broadcast(new PlayerHelmetChanged(evt.New.HasHelmet, evt.Previous.HasHelmet, evt.PlayerID));
            }

            if (!evt.New.FlashAmount.Equals(evt.Previous.FlashAmount))
            {
                dispatcher.Broadcast(new PlayerFlashAmountChanged(evt.New.FlashAmount, evt.Previous.FlashAmount, evt.PlayerID));
            }

            if (!evt.New.SmokedAmount.Equals(evt.Previous.SmokedAmount))
            {
                dispatcher.Broadcast(new PlayerSmokedAmountChanged(evt.New.SmokedAmount, evt.Previous.SmokedAmount, evt.PlayerID));
            }

            if (!evt.New.BurningAmount.Equals(evt.Previous.BurningAmount))
            {
                dispatcher.Broadcast(new PlayerBurningAmountChanged(evt.New.BurningAmount, evt.Previous.BurningAmount, evt.PlayerID));
            }

            if (!evt.New.Money.Equals(evt.Previous.Money))
            {
                dispatcher.Broadcast(new PlayerMoneyAmountChanged(evt.New.Money, evt.Previous.Money, evt.PlayerID));
            }

            if (!evt.New.RoundKills.Equals(evt.Previous.RoundKills))
            {
                dispatcher.Broadcast(new PlayerRoundKillsChanged(evt.New.RoundKills, evt.Previous.RoundKills, evt.PlayerID));
            }

            if (!evt.New.RoundHSKills.Equals(evt.Previous.RoundHSKills))
            {
                dispatcher.Broadcast(new PlayerRoundHeadshotKillsChanged(evt.New.RoundHSKills, evt.Previous.RoundHSKills, evt.PlayerID));
            }

            if (!evt.New.RoundTotalDamage.Equals(evt.Previous.RoundTotalDamage))
            {
                dispatcher.Broadcast(new PlayerRoundTotalDamageChanged(evt.New.RoundTotalDamage, evt.Previous.RoundTotalDamage, evt.PlayerID));
            }

            if (!evt.New.EquipmentValue.Equals(evt.Previous.EquipmentValue))
            {
                dispatcher.Broadcast(new PlayerEquipmentValueChanged(evt.New.EquipmentValue, evt.Previous.EquipmentValue, evt.PlayerID));
            }

            if (!evt.New.HasDefuseKit.Equals(evt.Previous.HasDefuseKit))
            {
                dispatcher.Broadcast(new PlayerDefusekitChanged(evt.New.HasDefuseKit, evt.Previous.HasDefuseKit, evt.PlayerID));
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
                dispatcher.Broadcast(new PlayerKillsChanged(evt.New.Kills, evt.Previous.Kills, evt.PlayerID));
            }

            if (!evt.New.Assists.Equals(evt.Previous.Assists))
            {
                dispatcher.Broadcast(new PlayerAssistsChanged(evt.New.Assists, evt.Previous.Assists, evt.PlayerID));
            }

            if (!evt.New.Deaths.Equals(evt.Previous.Deaths))
            {
                dispatcher.Broadcast(new PlayerDeathsChanged(evt.New.Deaths, evt.Previous.Deaths, evt.PlayerID));
            }

            if (!evt.New.MVPs.Equals(evt.Previous.MVPs))
            {
                dispatcher.Broadcast(new PlayerMVPsChanged(evt.New.MVPs, evt.Previous.MVPs, evt.PlayerID));
            }

            if (!evt.New.Score.Equals(evt.Previous.Score))
            {
                dispatcher.Broadcast(new PlayerScoreChanged(evt.New.Score, evt.Previous.Score, evt.PlayerID));
            }
        }
    }
}
