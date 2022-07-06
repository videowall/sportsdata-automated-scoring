using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateDeltaHandler : ILiveScoutEventHandler
    {
        Task Handle(MatchUpdateDelta data);
    }
}