namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchBookingReplyHandler : ILiveScoutEventHandler
    {
        void Handle(MatchBookingReply reply);
    }
}