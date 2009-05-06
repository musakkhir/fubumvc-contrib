using System.Linq;

namespace FubuMVC.Framework.Persistence
{
    public interface IDomainQuery<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> results);
    }
}