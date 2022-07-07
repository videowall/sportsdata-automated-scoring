using System;
using System.Threading.Tasks;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;
using WBH.Livescoring.Frontend.Logic.SportRadar.Bases;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.SportRadar;

internal sealed class LiveScoutMatchBookingReplyHandler : LiveScoutHandlerBase, ILiveScoutMatchBookingReplyHandler
{
    #region Fields

    private readonly IContext _context;

    #endregion

    #region Constructors

    public LiveScoutMatchBookingReplyHandler(IContext context): base(context)
    {
        _context = context;
    }

    #endregion

    #region ILiveScoutMatchBookingReplyHandler

    public async Task Handle(MatchBookingReply reply)
    {
        if (reply == null) return;

        var status = new Entities.Status {Happened = DateTime.UtcNow, MatchId = reply.MatchId, Message = reply.Message};
        if (reply.Result == BookMatchResult.Valid && reply.MatchId.HasValue)
        {
            var entity = await GetOrCreateMatchAsync(reply.MatchId.Value);
            entity.Status.Add(status);
            await _context.UpdateAsync(entity);
        }
        else if (reply.MatchId.HasValue == false)
        {
            await _context.UpdateAsync(status);
        }
    }

    #endregion
}