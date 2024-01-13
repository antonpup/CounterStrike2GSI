using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Auth update. 
    /// </summary>
    public class AuthUpdated : UpdateEvent<Auth>
    {
        public AuthUpdated(Auth new_value, Auth previous_value) : base(new_value, previous_value)
        {
        }
    }
}
