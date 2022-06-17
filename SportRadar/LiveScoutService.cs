using System;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Interfaces;

namespace WBH.Livescoring.SportRadar
{
    internal sealed class LiveScoutService : ILiveScoutService
    {
        #region Fields

        private readonly ILiveScout _liveScout;

        #endregion

        #region Constructors

        public LiveScoutService(ILiveScout liveScout)
        {
            _liveScout = liveScout;
        }

        #endregion

        #region ILiveScoutService

        public void SubscribeMatch(long matchId)
        {
            // TODO: LiveScout Booking
            throw new NotImplementedException();
        }

        #endregion
    }
}