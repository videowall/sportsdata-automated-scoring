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
        await _context.UpdateAsync(entity);
        
        foreach (var score in data.Scores)
        {
            var scoreDb = _context.Query<Entities.Score>().FirstOrDefault(s => s.MatchId == data.MatchId && s.Type.ToString() == score.Type);
            if (scoreDb != null)
            {
                _mapper.Map(score, scoreDb);
                await _context.UpdateAsync(scoreDb);
            }
            else
            {
                var newScore = _mapper.Map<Entities.Score>(score);
                newScore.MatchId = entity.Id;
                await _context.AddAsync(newScore);
            }
        }
    }
}