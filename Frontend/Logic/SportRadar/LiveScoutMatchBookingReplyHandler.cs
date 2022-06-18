using System;
using AutoMapper;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;
using WBH.Livescoring.Frontend.Logic.SportRadar.Bases;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.SportRadar;

internal sealed class LiveScoutMatchBookingReplyHandler : LiveScoutHandlerBase, ILiveScoutMatchBookingReplyHandler
{
    #region Fields

    private readonly IMapper _mapper;
    private readonly IContext _context;

    #endregion

    #region Constructors

    public LiveScoutMatchBookingReplyHandler(IMapper mapper, IContext context): base(context)
    {
        _mapper = mapper;
        _context = context;
    }

    #endregion

    #region ILiveScoutMatchBookingReplyHandler

    public void Handle(MatchBookingReply reply)
    {
        if (reply == null) return;

        var status = new Entities.Status {Happened = DateTime.UtcNow, MatchId = reply.MatchId, Message = reply.Message};
        if (reply.Result == BookMatchResult.Valid && reply.MatchId.HasValue)
        {
            var entity = GetMatch(reply.MatchId.Value);
            entity.Status.Add(status);
            _context.Save(entity);
        }
        else if (reply.MatchId.HasValue == false)
        {
            _context.Save(status);
        }
    }

    #endregion
}