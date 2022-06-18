namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchStopHandler : ILiveScoutEventHandler
    {
        void Handle(long matchId, string reason);
    }
}