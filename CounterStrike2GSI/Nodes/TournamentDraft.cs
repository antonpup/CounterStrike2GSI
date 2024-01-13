using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Enum list for each player team.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// The game is live.
        /// </summary>
        Live = 0
    }

    /// <summary>
    /// A class representing tournament raft information.
    /// </summary>
    public class TournamentDraft : Node
    {
        /// <summary>
        /// The game state.
        /// </summary>
        public readonly GameState State;

        /// <summary>
        /// The ID for this event.
        /// </summary>
        public readonly int EventID;

        /// <summary>
        /// The ID for this event's stage.
        /// </summary>
        public readonly int StageID;

        /// <summary>
        /// The ID of the first team.
        /// </summary>
        public readonly int FirstTeamID;

        /// <summary>
        /// The ID of the second team.
        /// </summary>
        public readonly int SecondTeamID;

        /// <summary>
        /// The name of this event.
        /// </summary>
        public readonly string Event;

        /// <summary>
        /// The name of the stage.
        /// </summary>
        public readonly string Stage;

        /// <summary>
        /// The name of the first team.
        /// </summary>
        public readonly string FirstTeamName;

        /// <summary>
        /// The name of the second team.
        /// </summary>
        public readonly string SecondTeamName;

        internal TournamentDraft(JObject parsed_data = null) : base(parsed_data)
        {
            State = GetEnum<GameState>("state");
            EventID = GetInt("eventid");
            StageID = GetInt("stageid");
            FirstTeamID = GetInt("teamid1");
            SecondTeamID = GetInt("teamid2");
            Event = GetString("event");
            Stage = GetString("stage");
            FirstTeamName = GetString("team1");
            SecondTeamName = GetString("team2");

        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"State: {State}, " +
                $"EventID: {EventID}, " +
                $"StageID: {StageID}, " +
                $"FirstTeamID: {FirstTeamID}, " +
                $"SecondTeamID: {SecondTeamID}, " +
                $"Event: {Event}, " +
                $"Stage: {Stage}, " +
                $"FirstTeamName: {FirstTeamName}, " +
                $"SecondTeamName: {SecondTeamName}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is TournamentDraft other &&
                State.Equals(other.State) &&
                EventID.Equals(other.EventID) &&
                StageID.Equals(other.StageID) &&
                FirstTeamID.Equals(other.FirstTeamID) &&
                SecondTeamID.Equals(other.SecondTeamID) &&
                Event.Equals(other.Event) &&
                Stage.Equals(other.Stage) &&
                FirstTeamName.Equals(other.FirstTeamName) &&
                SecondTeamName.Equals(other.SecondTeamName);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 988971238;
            hashCode = hashCode * -301854564 + State.GetHashCode();
            hashCode = hashCode * -301854564 + EventID.GetHashCode();
            hashCode = hashCode * -301854564 + StageID.GetHashCode();
            hashCode = hashCode * -301854564 + FirstTeamID.GetHashCode();
            hashCode = hashCode * -301854564 + SecondTeamID.GetHashCode();
            hashCode = hashCode * -301854564 + Event.GetHashCode();
            hashCode = hashCode * -301854564 + Stage.GetHashCode();
            hashCode = hashCode * -301854564 + FirstTeamName.GetHashCode();
            hashCode = hashCode * -301854564 + SecondTeamName.GetHashCode();
            return hashCode;
        }
    }
}
