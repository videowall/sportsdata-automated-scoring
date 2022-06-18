namespace WBH.Livescoring.SportRadar
{
    public class MatchListItem: Bases.MatchBase
    {
        public string CourtName { get; set; }
        public string TournamentName { get; set; }
        public ScoutMatchStatus MatchStatus { get; set; }
    }
}