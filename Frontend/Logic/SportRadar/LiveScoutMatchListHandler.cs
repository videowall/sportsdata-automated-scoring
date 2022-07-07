using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;
using WBH.Livescoring.Frontend.Logic.SportRadar.Bases;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.SportRadar;

internal sealed class LiveScoutMatchListHandler : LiveScoutHandlerBase, ILiveScoutMatchListHandler
{
    private readonly IMapper _mapper;
    private readonly IContext _context;

    public LiveScoutMatchListHandler(IMapper mapper, IContext context): base(context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task Handle(IEnumerable<MatchListItem> matches)
    {
        if (matches == null || matches.Any() == false) return;

        foreach (var match in matches)
        {
            var entity = await GetMatchAsync(match.MatchId);
            if (entity == null)
            {
                entity = new Entities.Match { Id = match.MatchId, Status = Entities.Status.Undefined };
                await _context.AddAsync(entity);
            }

            _mapper.Map(match, entity);
            await _context.UpdateAsync(entity);
        }
    }
}