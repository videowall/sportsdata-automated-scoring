using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public class MatchUpdateDeltaUpdate: Bases.MatchIdBase
    {
        #region Properties
        
        public Team Serve { get; set; }
        public ScoutMatchStatus? Status { get; set; }
        public IEnumerable<Score> Scores { get; set; }

        #endregion
    }
}