using System;

namespace WBH.Livescoring.Frontend.Logic.ScoreBoard;

internal sealed class ScoreBoardProvider : IScoreBoardProvider
{
    #region Constructors

    public ScoreBoardProvider() {}

    #endregion
    
    #region IScoreBoardProvider

    public ScoreBoardInfo GetScoreBoardInfo(long matchId)
    {
        throw new NotImplementedException();
    }

    public void BookMatch(long matchId)
    {
        throw new NotImplementedException();
    }

    #endregion
}