using System.Collections.Generic;
using WBH.Livescoring.SportRadar.Types;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateDeltaUpdateContract
    {
         Team Serve { get; set; }
         List<Score> Scores { get; set; }
    }
}