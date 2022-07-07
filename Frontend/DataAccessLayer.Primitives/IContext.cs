using System.Linq;
using System.Threading.Tasks;
using WBH.Livescoring.Frontend.Entities;

namespace WBH.Livescoring.Frontend.DataAccessLayer.Primitives;

public interface IContext
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
    void Save<TEntity>(TEntity entity) where TEntity : class, IEntity;
    Task SaveAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
    void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
    Task AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
    void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
    Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
    void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
    Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
}