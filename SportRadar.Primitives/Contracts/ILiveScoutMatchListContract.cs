using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchListContract
    {
        List<ILiveScoutMatchListItem> List { get; set; }
    }

    public interface ILiveScoutMatchListItem
    {
        string T1Name { get; set; }
        string T2Name { get; set; }
        long MatchId { get; set; } 
        string CourtName{ get; set; }
        string TournamentName { get; set; }
        ScoutMatchStatus MatchStatus { get; set; }
    }
}