using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public class MatchUpdate: Bases.MatchBase
    {
        #region Properties
        
        public string CourtName { get; set; }
        public string TournamentName { get; set; }
        public Team? Serve { get; set; }
        public ScoutMatchStatus? Status { get; set; }
        public IEnumerable<Score> Scores { get; set; }

        #endregion
    }
}