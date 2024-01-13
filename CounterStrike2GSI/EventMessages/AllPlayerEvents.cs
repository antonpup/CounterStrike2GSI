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
}
