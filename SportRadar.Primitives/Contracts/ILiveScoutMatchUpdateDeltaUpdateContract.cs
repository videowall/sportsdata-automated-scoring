using System.Collections.Generic;
using WBH.Livescoring.SportRadar.Types;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateDeltaUpdateContract
    {
         Team serve { get; set; }
         List<Score> scores { get; set; }
    }
}