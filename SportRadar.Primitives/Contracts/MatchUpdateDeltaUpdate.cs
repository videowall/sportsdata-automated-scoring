using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public class MatchUpdateDeltaUpdate
    {
        #region Properties

        public Team Serve { get; set; }
        public List<Score> Scores { get; set; }

        #endregion
    }
}