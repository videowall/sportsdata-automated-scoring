namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateHandler: ILiveScoutEventHandler
    {
        void Handle(MatchUpdate data);
    }
}