using CounterStrike2GSI.EventMessages;
using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI
{
    public class KillfeedHandler : EventHandler<CS2GameEvent>
    {
        private Player _last_killer = new Player();
        private Player _last_victim = new Player();
        private Weapon _killer_weapon = new Weapon();
        private bool _is_headshot;

        public KillfeedHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<PlayerDied>(OnPlayerDied);
            dispatcher.Subscribe<PlayerGotKill>(OnPlayerGotKill);
            dispatcher.Subscribe<RoundChanged>(OnRoundChanged);
        }

        ~KillfeedHandler()
        {
            dispatcher.Unsubscribe<PlayerDied>(OnPlayerDied);
            dispatcher.Unsubscribe<PlayerGotKill>(OnPlayerGotKill);
            dispatcher.Unsubscribe<RoundChanged>(OnRoundChanged);
        }

        private void OnPlayerDied(CS2GameEvent e)
        {
            PlayerDied evt = (e as PlayerDied);

            if (evt == null)
            {
                return;
            }

            _last_victim = evt.Player;

            ResolveKillFeed();
        }

        private void OnPlayerGotKill(CS2GameEvent e)
        {
            PlayerGotKill evt = (e as PlayerGotKill);

            if (evt == null)
            {
                return;
            }

            _last_killer = evt.Player;
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
            if (_last_killer.IsValid() && _last_victim.IsValid() && _killer_weapon.IsValid())
            {
                if (!_last_killer.SteamID.Equals(_last_victim.SteamID))
                {
                    dispatcher.Broadcast(new KillFeed(_last_killer, _last_victim, _is_headshot, _killer_weapon));
                }

                Reset();
            }
        }

        private void Reset()
        {
            _last_killer = new Player();
            _last_victim = new Player();
            _killer_weapon = new Weapon();
            _is_headshot = false;
        }
    }
}
