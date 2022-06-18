using System;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutClosedHandler : ILiveScoutEventHandler
    {
        void Handle(DateTime timestamp);
    }
}