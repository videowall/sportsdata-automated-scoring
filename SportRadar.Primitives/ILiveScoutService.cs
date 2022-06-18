namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutService
    {
        void Start();
        void Stop();
        void SubscribeMatch(long matchId);
    }
}