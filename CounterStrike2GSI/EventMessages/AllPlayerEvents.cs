using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for overall All Players update. 
    /// </summary>
    public class AllPlayersUpdated : UpdateEvent<AllPlayers>
    {
        public AllPlayersUpdated(AllPlayers new_value, AllPlayers previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Player disconnecting from the game. 
    /// </summary>
    public class PlayerDisconnected : ValueEvent<Player>
    {
        public PlayerDisconnected(Player value) : base(value)
        {
        }
    }

    /// <summary>
    /// Event for Player joining the game. 
    /// </summary>
    public class PlayerJoined : ValueEvent<Player>
    {
        public PlayerJoined(Player value) : base(value)
        {
        }
    }
}
