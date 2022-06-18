using System.Collections.Generic;
using WBH.Livescoring.SportRadar.Bases;

namespace WBH.Livescoring.SportRadar
{
    public class MatchData: MatchIdBase
    {
        #region Properties
        
        public string MatchTime { get; set; }
        private Dictionary<string, string> AdditionalData { get; set; }

        #endregion
    }
}