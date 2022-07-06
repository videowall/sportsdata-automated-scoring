using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchBookingReplyHandler : ILiveScoutEventHandler
    {
        Task Handle(MatchBookingReply reply);
    }
}