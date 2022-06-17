using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchListContract
    {
        List<ILiveScoutMatchListItem> list { get; set; }
    }

    public interface ILiveScoutMatchListItem
    {
        string t1Name { get; set; }
        string t2Name { get; set; }
        long matchId { get; set; } 
        string courtName{ get; set; }
        string tournamentName { get; set; }
        ScoutMatchStatus matchStatus { get; set; }
    }
}