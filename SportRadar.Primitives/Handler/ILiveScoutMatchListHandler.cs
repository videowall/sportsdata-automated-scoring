using System.Collections.Generic;
using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchListHandler : ILiveScoutEventHandler
    {
        Task Handle(IEnumerable<MatchListItem> matches);
    }
}