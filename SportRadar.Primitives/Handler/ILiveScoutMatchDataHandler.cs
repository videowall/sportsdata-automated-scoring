using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchDataHandler : ILiveScoutEventHandler
    {
        Task Handle(MatchData data);
    }
}