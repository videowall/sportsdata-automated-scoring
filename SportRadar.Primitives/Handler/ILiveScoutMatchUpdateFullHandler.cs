namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateFullHandler: ILiveScoutEventHandler
    {
        void Handle(ILiveScoutMatchUpdateContract data);
    }
}