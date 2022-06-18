using System.Linq;
using WBH.Livescoring.Frontend.Entities;

namespace WBH.Livescoring.Frontend.DataAccessLayer.Primitives;

public interface IContext
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
}