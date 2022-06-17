using System;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Interfaces;
using Sportradar.LiveData.Sdk.Services;

namespace WBH.Livescoring.SportRadar
{
    internal sealed class LiveScoutService : ILiveScoutService
    {
        #region Fields
        
        private readonly Sdk _sdk;
        private readonly Func<ILiveScout> _liveScoutFactory;

        #endregion

        #region Properties

        private ILiveScout LiveScout { get; set; }

        #endregion

        #region Constructors

        public LiveScoutService(Sdk sdk, Func<ILiveScout> liveScoutFactory)
        {
            _sdk = sdk;
            _liveScoutFactory = liveScoutFactory;
        }

        #endregion

        #region ILiveScoutService

        public void Start()
        {
            // Instanz starten
            _sdk.Start();

            // LiveScout abrufen
            LiveScout = _liveScoutFactory();

            // LiveScout starten
            LiveScout.Start();
        }

        public void Stop()
        {
            // LiveScout stoppen
            LiveScout.Stop();

            // Instanz Stoppen
            _sdk.Stop();
        }

        public void SubscribeMatch(long matchId)
        {
            // TODO: LiveScout Booking
            throw new NotImplementedException();
        }

        #endregion
    }
}