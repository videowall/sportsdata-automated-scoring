using System.Linq;
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
     
     protected Entities.Match GetOrCreateMatch(long matchId) => _context.Query<Entities.Match>().FirstOrDefault(e => e.Id == matchId) ?? new Entities.Match{Id=matchId};
}