using System.Linq;

namespace FubuMVC.Framework.Persistence
{
    public interface IRepository<TId>
    {
        void Save(object entity);

        TEntity Load<TEntity>(TId id)
            where TEntity : class;

        IQueryable<TEntity> Query<TEntity>()
            where TEntity : class;

        IQueryable<TEntity> Query<TEntity>(IDomainQuery<TEntity> whereQuery)
            where TEntity : class;
    }
}