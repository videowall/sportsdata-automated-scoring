namespace WBH.Livescoring.SportRadar
{
    public class MatchUpdateDelta: Bases.MatchBase
    {
        #region Properties
        
        public string CourtName { get; set; }
        public string TournamentName { get; set; }
        public ScoutMatchStatus? Status { get; set; }
        public Team? Serve { get; set; }

        #endregion
    }
}