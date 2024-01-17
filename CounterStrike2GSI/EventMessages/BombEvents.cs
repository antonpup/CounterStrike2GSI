using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Bomb update. 
    /// </summary>
    public class BombUpdated : UpdateEvent<Bomb>
    {
        public BombUpdated(Bomb new_value, Bomb previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Bomb being planted. 
    /// </summary>
    public class BombPlanting : CS2GameEvent
    {
        /// <summary>
        /// The player planting the bomb.
        /// </summary>
        public readonly Player Player;

        public BombPlanting(Player player) : base()
        {
            Player = player;
        }
    }

    /// <summary>
    /// Event for Bomb having been planted. 
    /// </summary>
    public class BombPlanted : CS2GameEvent
    {
        public BombPlanted() : base()
        {
        }
    }

    /// <summary>
    /// Event for Bomb having been defused. 
    /// </summary>
    public class BombDefused : CS2GameEvent
    {
        public BombDefused() : base()
        {
        }
    }

    /// <summary>
    /// Event for Bomb being defused. 
    /// </summary>
    public class BombDefusing : CS2GameEvent
    {
        /// <summary>
        /// The player defusing the bomb.
        /// </summary>
        public readonly Player Player;

        public BombDefusing(Player player) : base()
        {
            Player = player;
        }
    }

    /// <summary>
    /// Event for Bomb having been dropped. 
    /// </summary>
    public class BombDropped : CS2GameEvent
    {
        public BombDropped() : base()
        {
        }
    }

    /// <summary>
    /// Event for Bomb having been picked up. 
    /// </summary>
    public class BombPickedup : CS2GameEvent
    {
        /// <summary>
        /// The player carrying the bomb.
        /// </summary>
        public readonly Player Player;

        public BombPickedup(Player player) : base()
        {
            Player = player;
        }
    }

    /// <summary>
    /// Event for Bomb detonating. 
    /// </summary>
    public class BombExploded : CS2GameEvent
    {
        public BombExploded() : base()
        {
        }
    }
}
