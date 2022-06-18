namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchDataHandler : ILiveScoutEventHandler
    {
        void Handle(MatchData data);
    }
}