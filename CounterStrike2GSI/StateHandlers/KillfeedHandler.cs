using CounterStrike2GSI.EventMessages;
using CounterStrike2GSI.Nodes;
using System.Collections.Generic;

namespace CounterStrike2GSI
{
    public class KillfeedHandler : EventHandler<CS2GameEvent>
    {
        private Dictionary<string, Player> _player_cache = new Dictionary<string, Player>();

        private string _last_killer;
        private string _last_victim;
        private Weapon _killer_weapon;
        private bool _is_headshot;

        public KillfeedHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<PlayerUpdated>(OnPlayerUpdated);
            dispatcher.Subscribe<PlayerDied>(OnPlayerDied);
            dispatcher.Subscribe<PlayerGotKill>(OnPlayerGotKill);
            dispatcher.Subscribe<RoundChanged>(OnRoundChanged);
        }

        ~KillfeedHandler()
        {
            dispatcher.Unsubscribe<PlayerUpdated>(OnPlayerUpdated);
            dispatcher.Unsubscribe<PlayerDied>(OnPlayerDied);
            dispatcher.Unsubscribe<PlayerGotKill>(OnPlayerGotKill);
            dispatcher.Unsubscribe<RoundChanged>(OnRoundChanged);
        }

        private void OnPlayerUpdated(CS2GameEvent e)
        {
            PlayerUpdated evt = (e as PlayerUpdated);

            if (evt == null)
            {
                return;
            }

            _player_cache[evt.PlayerID] = evt.New;
        }

        private void OnPlayerDied(CS2GameEvent e)
        {
            PlayerDied evt = (e as PlayerDied);

            if (evt == null)
            {
                return;
            }

            _last_victim = evt.PlayerID;

            ResolveKillFeed();
        }

        private void OnPlayerGotKill(CS2GameEvent e)
        {
            PlayerGotKill evt = (e as PlayerGotKill);

            if (evt == null)
            {
                return;
            }

            _last_killer = evt.PlayerID;
            _killer_weapon = evt.Weapon;
            _is_headshot = evt.IsHeadshot;

            ResolveKillFeed();
        }

        private void OnRoundChanged(CS2GameEvent e)
        {
            RoundChanged evt = (e as RoundChanged);

            if (evt == null)
            {
                return;
            }

            Reset();
        }

        private void ResolveKillFeed()
        {
            if (!string.IsNullOrWhiteSpace(_last_killer) && !string.IsNullOrWhiteSpace(_last_victim) && _killer_weapon.IsValid())
            {
                dispatcher.Broadcast(new KillFeed(_player_cache[_last_killer], _player_cache[_last_victim], _is_headshot, _killer_weapon));

                Reset();
            }
        }

        private void Reset()
        {
            _last_killer = "";
            _last_victim = "";
            _killer_weapon = new Weapon();
            _is_headshot = false;
        }
    }
}
