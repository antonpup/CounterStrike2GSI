using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Phase Countdowns update. 
    /// </summary>
    public class PhaseCountdownsUpdated : UpdateEvent<PhaseCountdowns>
    {
        public PhaseCountdownsUpdated(PhaseCountdowns new_value, PhaseCountdowns previous_value) : base(new_value, previous_value)
        {
        }
    }
}
