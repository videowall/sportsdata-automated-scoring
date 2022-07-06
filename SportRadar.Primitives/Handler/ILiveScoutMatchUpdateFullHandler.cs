using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateFullHandler : ILiveScoutEventHandler
    {
        Task Handle(MatchUpdate data);
    }
}