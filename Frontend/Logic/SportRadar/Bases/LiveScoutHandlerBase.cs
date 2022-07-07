using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.SportRadar.Bases;

internal abstract class LiveScoutHandlerBase: ILiveScoutEventHandler
{
     private readonly IContext _context;
     
     protected LiveScoutHandlerBase(IContext context)
     {
          _context = context;
     }
     
     protected Entities.Match GetOrCreateMatch(long matchId) => _context.Query<Entities.Match>().Include(e => e.Scores).FirstOrDefault(e => e.Id == matchId) ?? new Entities.Match{Id=matchId,Status=Entities.Status.Undefined};
     protected async Task<Entities.Match> GetOrCreateMatchAsync(long matchId) => await _context.Query<Entities.Match>().Include(e => e.Scores).FirstOrDefaultAsync(e => e.Id == matchId) ?? new Entities.Match{Id=matchId,Status=Entities.Status.Undefined};
     protected Entities.Match GetMatch(long matchId) => _context.Query<Entities.Match>().Include(e => e.Scores).FirstOrDefault(e => e.Id == matchId);
     protected async Task<Entities.Match> GetMatchAsync(long matchId) => await _context.Query<Entities.Match>().Include(e => e.Scores).FirstOrDefaultAsync(e => e.Id == matchId);
}