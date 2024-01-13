using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Round update. 
    /// </summary>
    public class RoundUpdated : UpdateEvent<Round>
    {
        public RoundUpdated(Round new_value, Round previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Round Phase update.
    /// </summary>
    public class RoundPhaseUpdated : UpdateEvent<Phase>
    {
        public RoundPhaseUpdated(Phase new_value, Phase previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Bomb State update.
    /// </summary>
    public class BombStateUpdated : UpdateEvent<BombState>
    {
        public BombStateUpdated(BombState new_value, BombState previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Round victory.
    /// </summary>
    public class TeamRoundVictory : TeamValueEvent<int>
    {
        public TeamRoundVictory(int value, PlayerTeam team) : base(value, team)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Round loss.
    /// </summary>
    public class TeamRoundLoss : TeamValueEvent<int>
    {
        public TeamRoundLoss(int value, PlayerTeam team) : base(value, team)
        {
        }
    }
}
