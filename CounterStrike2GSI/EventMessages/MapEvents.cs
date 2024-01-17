using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Map update. 
    /// </summary>
    public class MapUpdated : UpdateEvent<Map>
    {
        public MapUpdated(Map new_value, Map previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Statistics update.
    /// </summary>
    public class TeamStatisticsUpdated : TeamUpdateEvent<TeamMapStatistics>
    {
        public TeamStatisticsUpdated(TeamMapStatistics new_value, TeamMapStatistics previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }

    /// <summary>
    /// Event for round change.
    /// </summary>
    public class RoundChanged : UpdateEvent<int>
    {
        public RoundChanged(int new_value, int previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for round conclusion.
    /// </summary>
    public class RoundConcluded : CS2GameEvent
    {
        /// <summary>
        /// The round.
        /// </summary>
        public readonly int Round;

        /// <summary>
        /// The reason for round conclusion.
        /// </summary>
        public readonly RoundConclusion RoundConclusionReason;

        /// <summary>
        /// The winning team of the round.
        /// </summary>
        public readonly PlayerTeam WinningTeam;

        /// <summary>
        /// Is this the first round?
        /// </summary>
        public readonly bool IsFirstRound;

        /// <summary>
        /// Is this the last round (or last round of half)?
        /// </summary>
        public readonly bool IsLastRound;

        public RoundConcluded(int round, RoundConclusion conclusion, PlayerTeam winning_team, bool is_first_round, bool is_last_round) : base()
        {
            Round = round;
            RoundConclusionReason = conclusion;
            WinningTeam = winning_team;
            IsFirstRound = is_first_round;
            IsLastRound = is_last_round;
        }
    }

    /// <summary>
    /// Event for round starting.
    /// </summary>
    public class RoundStarted : CS2GameEvent
    {
        /// <summary>
        /// The round.
        /// </summary>
        public readonly int Round;

        /// <summary>
        /// Is this the first round?
        /// </summary>
        public readonly bool IsFirstRound;

        /// <summary>
        /// Is this the last round (or last round of half)?
        /// </summary>
        public readonly bool IsLastRound;

        public RoundStarted(int round, bool is_first_round, bool is_last_round) : base()
        {
            Round = round;
            IsFirstRound = is_first_round;
            IsLastRound = is_last_round;
        }
    }

    /// <summary>
    /// Event for level change.
    /// </summary>
    public class LevelChanged : UpdateEvent<string>
    {
        public LevelChanged(string new_value, string previous_value) : base(new_value, previous_value)
        {
        }
    }
}
