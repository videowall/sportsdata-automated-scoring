using System;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Interfaces;
using Sportradar.LiveData.Sdk.Services;

namespace WBH.Livescoring.SportRadar;

internal sealed class LiveScoutService : ILiveScoutService
{
    #region Constructors

    public LiveScoutService(Sdk sdk, Func<ILiveScout> liveScoutFactory)
    {
        _sdk = sdk;
        _liveScout = new Lazy<ILiveScout>(liveScoutFactory);
    }

    #endregion

    #region Fields

    private readonly Sdk _sdk;
    private readonly Lazy<ILiveScout> _liveScout;

    #endregion

    #region ILiveScoutService

    public void Start()
    {
        // Instanz starten
        _sdk.Start();

        // LiveScout starten
        _liveScout.Value.Start();
    }

    public void Stop()
    {
        // LiveScout stoppen
        _liveScout.Value.Stop();

        // Instanz Stoppen
        _sdk.Stop();
    }

    public void SubscribeMatch(long matchId)
    {
        _liveScout.Value.Subscribe(new []{matchId});
    }

    #endregion
}