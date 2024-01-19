using CounterStrike2GSI.EventMessages;
using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI
{
    public class MapHandler : EventHandler<CS2GameEvent>
    {
        private int _max_rounds = 24; // Hardcoded to 24

        public MapHandler(ref EventDispatcher<CS2GameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<MapUpdated>(OnMapUpdated);
        }

        ~MapHandler()
        {
            dispatcher.Unsubscribe<MapUpdated>(OnMapUpdated);
        }

        private void OnMapUpdated(CS2GameEvent e)
        {
            MapUpdated evt = (e as MapUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.Name.Equals(evt.Previous.Name))
            {
                dispatcher.Broadcast(new LevelChanged(evt.New.Name, evt.Previous.Name));
            }

            if (!evt.New.CTStatistics.Equals(evt.Previous.CTStatistics))
            {
                dispatcher.Broadcast(new TeamStatisticsUpdated(evt.New.CTStatistics, evt.Previous.CTStatistics, Nodes.PlayerTeam.CT));
            }

            if (!evt.New.TStatistics.Equals(evt.Previous.TStatistics))
            {
                dispatcher.Broadcast(new TeamStatisticsUpdated(evt.New.TStatistics, evt.Previous.TStatistics, Nodes.PlayerTeam.T));
            }

            bool is_last_round = (((evt.New.Round + 1) == _max_rounds) || ((evt.New.Round + 1) / (float)_max_rounds) == 0.5f); // Next round is half
            bool is_first_round = ((evt.New.Round == 0) || (evt.New.Round / (float)_max_rounds) == 0.5f); // Is first round or half round

            if (!evt.New.Round.Equals(evt.Previous.Round))
            {
                dispatcher.Broadcast(new RoundChanged(evt.New.Round, evt.Previous.Round));

                if (evt.New.Round > evt.Previous.Round)
                {
                    if (evt.New.Round != 0)
                    {
                        var round_conclusion = evt.New.RoundWins[evt.Previous.Round + 1]; // RoundWins is off by one. Where RoundWins[1] == Round 0.
                        PlayerTeam winning_team = PlayerTeam.Undefined;

                        switch (round_conclusion)
                        {
                            case RoundConclusion.T_Win_Elimination:
                            case RoundConclusion.T_Win_Time:
                            case RoundConclusion.T_Win_Bomb:
                                winning_team = PlayerTeam.T;
                                break;
                            case RoundConclusion.CT_Win_Elimination:
                            case RoundConclusion.CT_Win_Time:
                            case RoundConclusion.CT_Win_Defuse:
                            case RoundConclusion.CT_Win_Rescue:
                                winning_team = PlayerTeam.CT;
                                break;
                            default:
                                break;
                        }

                        dispatcher.Broadcast(new RoundConcluded(evt.Previous.Round, round_conclusion, winning_team, is_first_round, is_last_round));
                    }

                    dispatcher.Broadcast(new RoundStarted(evt.New.Round, is_first_round, is_last_round));
                }
            }
        }
    }
}
