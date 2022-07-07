using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchStopHandler : ILiveScoutEventHandler
    {
        Task Handle(MatchStop data);
    }
}