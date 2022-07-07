namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutService
    {
        void Start();
        void Stop();
        void SubscribeMatch(long matchId);
        void GetMatchList(int hoursBack, int hoursForwarded, bool includeAvailable = false);
    }
}