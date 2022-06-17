using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchDataHandler: ILiveScoutEventHandler
    {
        void Handle(long matchId, string matchTime, Dictionary<string, string> additionalData);
    }
}