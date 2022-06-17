namespace WBH.Livescoring.SportRadar
{
    public enum BookMatchResult
    {
        UNDEFINED,
        VALID,
        INVALID,
    }
    
    public interface ILiveScoutMatchBookingReplyHandler : ILiveScoutEventHandler
    {
        void Handle(long? matchId, string message, BookMatchResult result);
    }
}