namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateHandler: ILiveScoutEventHandler
    {
        void Handle(ILiveScoutMatchUpdateContract data);
    }
}