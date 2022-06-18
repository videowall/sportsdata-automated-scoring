using System;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.ScoreBoard;

internal sealed class ScoreBoardProvider : IScoreBoardProvider
{
    #region Fields

    private readonly ILiveScoutService _liveScoutService;

    #endregion

    #region Constructors

    public ScoreBoardProvider(ILiveScoutService liveScoutService)
    {
        _liveScoutService = liveScoutService;
    }

    #endregion
    
    #region IScoreBoardProvider

    public ScoreBoardInfo GetScoreBoardInfo(long matchId)
    {
        throw new NotImplementedException();
    }

    public void BookMatch(long matchId)
    {
        _liveScoutService.SubscribeMatch(matchId);
    }

    #endregion
}