namespace WBH.Livescoring.Frontend.Logic.ScoreBoard;

public interface IScoreBoardProvider
{
    ScoreBoardInfo GetScoreBoardInfo(long matchId);
}