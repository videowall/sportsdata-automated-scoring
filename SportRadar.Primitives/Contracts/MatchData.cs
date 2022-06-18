using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public class MatchData
    {
        #region Properties

        public long MatchId { get; set; }
        public string MatchTime { get; set; }
        private Dictionary<string, string> AdditionalData { get; set; }

        #endregion
    }
}