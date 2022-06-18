using System;
using System.Linq;
using System.Linq.Expressions;

namespace WBH.Livescoring.Frontend.Logic.ScoreBoard;

public class ScoreBoardInfo
{
    #region Properties

    public string Team1Line1 {get;set;}
    public string Team1Line2 {get;set;}
    public string Team2Line1 {get;set;}
    public string Team2Line2 {get;set;}
    public string Team1Set1 {get;set;}
    public string Team1Set2 {get;set;}
    public string Team1Set3 {get;set;}
    public string Team1Set4 {get;set;}
    public string Team1Set5 {get;set;}
    public string Team2Set1 {get;set;}
    public string Team2Set2 {get;set;}
    public string Team2Set3 {get;set;}
    public string Team2Set4 {get;set;}
    public string Team2Set5 {get;set;}
    public string PointsTeam1 {get;set;}
    public string PointsTeam2 {get;set;}
    public string ServiceHome {get;set;}
    public string ServiceAway {get;set;}
    public string MatchTime {get;set;}

    #endregion

    #region Projections

    internal static Expression<Func<Entities.Match, ScoreBoardInfo>> Project()
    {
        return x => new ScoreBoardInfo
        {
            MatchTime = x.MatchTime,
            Team1Line1 = x.Team1Line1,
            Team1Line2 = x.Team1Line2,
            Team2Line1 = x.Team2Line1,
            Team2Line2 = x.Team2Line2,
            Team1Set1 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set1).Sum(s => s.Team1).ToString("0"),
            Team2Set1 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set1).Sum(s => s.Team2).ToString("0"),
            Team1Set2 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set2).Sum(s => s.Team1).ToString("0"),
            Team2Set2 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set2).Sum(s => s.Team2).ToString("0"),
            Team1Set3 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set3).Sum(s => s.Team1).ToString("0"),
            Team2Set3 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set3).Sum(s => s.Team2).ToString("0"),
            Team1Set4 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set4).Sum(s => s.Team1).ToString("0"),
            Team2Set4 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set4).Sum(s => s.Team2).ToString("0"),
            Team1Set5 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set5).Sum(s => s.Team1).ToString("0"),
            Team2Set5 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Set5).Sum(s => s.Team2).ToString("0"),
            PointsTeam1 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Game).Sum(s => s.Team1).ToString("0"),
            PointsTeam2 = x.Scores.AsQueryable().Where(s => s.Type == Entities.ScoreType.Game).Sum(s => s.Team2).ToString("0"),
            ServiceHome = x.Service == Entities.HomeAway.Home ? "PFAD" : "",
            ServiceAway = x.Service == Entities.HomeAway.Away ? "PFAD" : ""
        };
    }

    #endregion
}