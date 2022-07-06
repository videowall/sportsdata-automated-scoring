using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateDeltaUpdateHandler : ILiveScoutEventHandler
    {
        Task Handle(MatchUpdateDeltaUpdate data);
    }
}