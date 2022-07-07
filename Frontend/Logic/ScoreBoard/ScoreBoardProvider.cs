using System.Linq;
using Microsoft.EntityFrameworkCore;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.ScoreBoard;

internal sealed class ScoreBoardProvider : IScoreBoardProvider
{
    #region Fields

    private readonly ILiveScoutService _liveScoutService;
    private readonly IContext _context;

    #endregion

    #region Constructors

    public ScoreBoardProvider(ILiveScoutService liveScoutService, IContext context)
    {
        _liveScoutService = liveScoutService;
        _context = context;
    }

    #endregion
    
    #region IScoreBoardProvider

    public ScoreBoardInfo GetScoreBoardInfo(long matchId)
    {
        return _context.Query<Entities.Match>()
            .Where(e => e.Id == matchId)
            .Include(e => e.Scores)
            .Select(x => new ScoreBoardInfo
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
            })
            .FirstOrDefault();
    }

    public void BookMatch(long matchId)
    {
        if (_context.Query<Entities.Match>().Any(e => e.Id == matchId) == false)
        {
            _context.Add(new Entities.Match { Id = matchId });
        }
        
        _liveScoutService.SubscribeMatch(matchId);
    }

    #endregion
}