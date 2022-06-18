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
            .Include(e => e.Scores)
            .Select(ScoreBoardInfo.Project)
            .FirstOrDefault();
    }

    public void BookMatch(long matchId)
    {
        _liveScoutService.SubscribeMatch(matchId);
    }

    #endregion
}