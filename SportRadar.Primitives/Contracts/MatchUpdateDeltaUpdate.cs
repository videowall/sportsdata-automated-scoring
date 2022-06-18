using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public class MatchUpdateDeltaUpdate
    {
        #region Properties

        public long MatchId { get; set; }
        public Team Serve { get; set; }
        public IEnumerable<Score> Scores { get; set; }

        #endregion
    }
}