
using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for specific player update.
    /// </summary>
    public class PlayerEvent : CS2GameEvent
    {
        /// <summary>
        /// The associated player ID.
        /// </summary>
        public readonly string PlayerID;

        public PlayerEvent(string player_id) : base()
        {
            PlayerID = player_id;
        }
    }

    /// <summary>
    /// Event for a single value update.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class ValueEvent<T> : CS2GameEvent
    {
        /// <summary>
        /// Value.
        /// </summary>
        public readonly T Value;

        public ValueEvent(T obj)
        {
            Value = obj;
        }
    }

    /// <summary>
    /// Event for specific player's single value update.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class PlayerValueEvent<T> : ValueEvent<T>
    {
        /// <summary>
        /// The associated player ID.
        /// </summary>
        public readonly string PlayerID;

        public PlayerValueEvent(T obj, string player_id) : base(obj)
        {
            PlayerID = player_id;
        }
    }

    /// <summary>
    /// Event for specific team's single value update.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class TeamValueEvent<T> : ValueEvent<T>
    {
        /// <summary>
        /// The associated team.
        /// </summary>
        public readonly PlayerTeam Team;

        public TeamValueEvent(T obj, PlayerTeam team) : base(obj)
        {
            Team = team;
        }
    }

    /// <summary>
    /// Event for value change.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class UpdateEvent<T> : CS2GameEvent
    {
        /// <summary>
        /// New value.
        /// </summary>
        public readonly T New;

        /// <summary>
        /// Previous value.
        /// </summary>
        public readonly T Previous;

        public UpdateEvent(T new_obj, T previous_obj)
        {
            New = new_obj;
            Previous = previous_obj;
        }
    }

    /// <summary>
    /// Event for specific player's value change.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class PlayerUpdateEvent<T> : UpdateEvent<T>
    {
        /// <summary>
        /// The associated player ID.
        /// </summary>
        public readonly string PlayerID;

        public PlayerUpdateEvent(T new_obj, T previous_obj, string player_id) : base(new_obj, previous_obj)
        {
            PlayerID = player_id;
        }
    }

    /// <summary>
    /// Event for specific team's value change.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TeamUpdateEvent<T> : UpdateEvent<T>
    {
        /// <summary>
        /// The associated team.
        /// </summary>
        public readonly PlayerTeam Team;

        public TeamUpdateEvent(T new_obj, T previous_obj, PlayerTeam team) : base(new_obj, previous_obj)
        {
            Team = team;
        }
    }

    /// <summary> 
    /// Event for specific entity's value change. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    public class EntityUpdateEvent<T> : UpdateEvent<T>
    {
        /// <summary> 
        /// The associated entity ID. 
        /// </summary> 
        public readonly string EntityID;

        public EntityUpdateEvent(T new_obj, T previous_obj, string entity_id) : base(new_obj, previous_obj)
        {
            EntityID = entity_id;
        }
    }
}
