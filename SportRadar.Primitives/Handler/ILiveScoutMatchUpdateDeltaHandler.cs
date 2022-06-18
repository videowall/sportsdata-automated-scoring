namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateDeltaHandler : ILiveScoutEventHandler
    {
        void Handle(MatchUpdateDelta data);
    }
}