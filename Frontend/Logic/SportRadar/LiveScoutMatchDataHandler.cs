using AutoMapper;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;
using WBH.Livescoring.Frontend.Logic.SportRadar.Bases;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.SportRadar;

internal sealed class LiveScoutMatchDataHandler:LiveScoutHandlerBase, ILiveScoutMatchDataHandler
{
    private readonly IMapper _mapper;
    private readonly IContext _context;

    public LiveScoutMatchDataHandler(IMapper mapper, IContext context): base(context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public void Handle(MatchData data)
    {
        if (data == null) return;
        
        var entity = GetOrCreateMatch(data.MatchId);
        _mapper.Map(data, entity);
        _context.Save(entity);
    }
}