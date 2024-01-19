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
    /// Event for Player connecting to the game. 
    /// </summary>
    public class PlayerConnected : ValueEvent<Player>
    {
        public PlayerConnected(Player value) : base(value)
        {
        }
    }
}
