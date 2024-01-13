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
}
