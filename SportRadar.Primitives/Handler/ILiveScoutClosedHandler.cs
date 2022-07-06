using System;
using System.Threading.Tasks;

namespace WBH.Livescoring.SportRadar
{
    public interface ILiveScoutClosedHandler : ILiveScoutEventHandler
    {
        Task Handle(DateTime timestamp);
    }
}