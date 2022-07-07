using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;
using WBH.Livescoring.Frontend.Logic.SportRadar.Bases;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.SportRadar;

internal sealed class LiveScoutMatchUpdateDeltaUpdateHandler: LiveScoutHandlerBase, ILiveScoutMatchUpdateDeltaUpdateHandler
{
    private readonly IMapper _mapper;
    private readonly IContext _context;

    public LiveScoutMatchUpdateDeltaUpdateHandler(IMapper mapper, IContext context): base(context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task Handle(MatchUpdateDeltaUpdate data)
    {
        if (data == null) return;
        
        var entity = await GetMatchAsync(data.MatchId);
        _mapper.Map(data, entity);
        var scores = _mapper.Map<IList<Entities.Score>>(data.Scores);
        foreach (var score in scores)
        {
            var scoreSet = entity.Scores.FirstOrDefault(s => s.Type == score.Type);
            if (scoreSet != null)
            {
                entity.Scores.Remove(scoreSet);
            }
            entity.Scores.Add(score);
        }
        await _context.UpdateAsync(entity);
    }
}