namespace WBH.Livescoring.SportRadar
{
    public class MatchUpdate
    {
        #region Properties

        public string T1Name { get; set; }
        public string T2Name { get; set; }
        public long MatchId { get; set; }
        public string CourtName { get; set; }
        public string TournamentName { get; set; }
        public Team? Serve { get; set; }

        #endregion
    }
}