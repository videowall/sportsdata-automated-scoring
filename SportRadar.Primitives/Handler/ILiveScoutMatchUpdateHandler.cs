using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateHandler : ILiveScoutEventHandler
    {
        Task Handle(MatchUpdate data);
    }
}