namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutMatchUpdateDeltaUpdateHandler: ILiveScoutEventHandler
    {
        void Handle(ILiveScoutMatchUpdateDeltaUpdateContract data);
    }
}