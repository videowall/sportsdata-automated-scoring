using System.Collections.Generic;

namespace WBH.Livescoring.Frontend.Logic.ScoreBoard;

public interface IScoreBoardProvider
{
    ScoreBoardInfo GetScoreBoardInfo(long matchId);
    void BookMatch(long matchId);
    IEnumerable<Match> GetAvailableMatches();
}