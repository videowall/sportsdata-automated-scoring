using WBH.Livescoring.SportRadar.Types;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateContract
    {
        string T1Name { get; set; }
        string T2Name { get; set; }
        long MatchId { get; set; }
        string CourtName{ get; set; }
        string TournamentName { get; set; }
            Team Serve { get; set; }
    }
}