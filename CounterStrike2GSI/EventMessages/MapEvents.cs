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
    /// Event for gamemode changing. 
    /// </summary>
    public class GamemodeChanged : UpdateEvent<GameMode>
    {
        public GamemodeChanged(GameMode new_value, GameMode previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Statistics update.
    /// </summary>
    public class TeamStatisticsUpdated : TeamUpdateEvent<TeamStatistics>
    {
        public TeamStatisticsUpdated(TeamStatistics new_value, TeamStatistics previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }

    /// <summary>
    /// Event for specific team's score change.
    /// </summary>
    public class TeamScoreChanged : TeamUpdateEvent<int>
    {
        public TeamScoreChanged(int new_value, int previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }

    /// <summary>
    /// Event for specific team's remaining timeouts change.
    /// </summary>
    public class TeamRemainingTimeoutsChanged : TeamUpdateEvent<int>
    {
        public TeamRemainingTimeoutsChanged(int new_value, int previous_value, PlayerTeam team) : base(new_value, previous_value, team)
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

    /// <summary>
    /// Event for Map phase change.
    /// </summary>
    public class MapPhaseChanged : UpdateEvent<Phase>
    {
        public MapPhaseChanged(Phase new_value, Phase previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for game Warmup starting.
    /// </summary>
    public class WarmupStarted : CS2GameEvent
    {
        public WarmupStarted() : base()
        {
        }
    }

    /// <summary>
    /// Event for game Warmup ending.
    /// </summary>
    public class WarmupOver : CS2GameEvent
    {
        public WarmupOver() : base()
        {
        }
    }

    /// <summary>
    /// Event for game Intermission (or half-time intermission) starting.
    /// </summary>
    public class IntermissionStarted : CS2GameEvent
    {
        public IntermissionStarted() : base()
        {
        }
    }

    /// <summary>
    /// Event for game Intermission (or half-time intermission) ending.
    /// </summary>
    public class IntermissionOver : CS2GameEvent
    {
        public IntermissionOver() : base()
        {
        }
    }

    /// <summary>
    /// Event for game Freezetime starting.
    /// </summary>
    public class FreezetimeStarted : CS2GameEvent
    {
        public FreezetimeStarted() : base()
        {
        }
    }

    /// <summary>
    /// Event for game Freezetime ending.
    /// </summary>
    public class FreezetimeOver : CS2GameEvent
    {
        public FreezetimeOver() : base()
        {
        }
    }

    /// <summary>
    /// Event for game Pause starting.
    /// </summary>
    public class PauseStarted : CS2GameEvent
    {
        public PauseStarted() : base()
        {
        }
    }

    /// <summary>
    /// Event for game Pause ending.
    /// </summary>
    public class PauseOver : CS2GameEvent
    {
        public PauseOver() : base()
        {
        }
    }

    /// <summary>
    /// Event for game Timeout starting.
    /// </summary>
    public class TimeoutStarted : CS2GameEvent
    {
        /// <summary>
        /// The team the timeout is started by.
        /// </summary>
        public readonly PlayerTeam Team;

        public TimeoutStarted(PlayerTeam team) : base()
        {
            Team = team;
        }
    }

    /// <summary>
    /// Event for game Timeout ending.
    /// </summary>
    public class TimeoutOver : CS2GameEvent
    {
        /// <summary>
        /// The team the timeout is started by.
        /// </summary>
        public readonly PlayerTeam Team;

        public TimeoutOver(PlayerTeam team) : base()
        {
            Team = team;
        }
    }

    /// <summary>
    /// Event for game Match starting (or resuming).
    /// </summary>
    public class MatchStarted : CS2GameEvent
    {
        public MatchStarted() : base()
        {
        }
    }

    /// <summary>
    /// Event for game being over.
    /// </summary>
    public class Gameover : CS2GameEvent
    {
        public Gameover() : base()
        {
        }
    }
}
