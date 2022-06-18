namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchStopHandler: ILiveScoutEventHandler
    {
        void Handle(string matchId, string reason);
    }
}