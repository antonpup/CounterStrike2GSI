using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for player killfeed.
    /// </summary>
    public class KillFeed : CS2GameEvent
    {
        /// <summary>
        /// The killer.
        /// </summary>
        public readonly Player Killer;

        /// <summary>
        /// The victim.
        /// </summary>
        public readonly Player Victim;

        /// <summary>
        /// Was the kill a headshot?
        /// </summary>
        public readonly bool IsHeadshot;

        /// <summary>
        /// The weapon used to kill.
        /// </summary>
        public readonly Weapon Weapon;

        public KillFeed(Player killer, Player victim, bool is_headshot, Weapon weapon) : base()
        {
            Killer = killer;
            Victim = victim;
            IsHeadshot = is_headshot;
            Weapon = weapon;
        }
    }
}
