using System.Collections.Generic;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchListHandler : ILiveScoutEventHandler
    {
        void Handle(IEnumerable<MatchListItem> matches);
    }
}