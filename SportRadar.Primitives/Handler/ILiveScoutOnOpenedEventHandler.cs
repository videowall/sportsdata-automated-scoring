using System;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutOnOpenedEventHandler : ILiveScoutEventHandler
    {
        void Handle(DateTime timestamp);
    }
}