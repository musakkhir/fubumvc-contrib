using System;
using System.Linq;
using FubuMVC.Framework.Persistence;
using NHibernate.Linq;

namespace FubuMVC.Framework.NHibernate
{
    public class NHibernateRepository<TId> : IRepository<TId>
    {
        private readonly INHibernateUnitOfWork _unitOfWork;

        public NHibernateRepository(INHibernateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
      
        public void Save(object entity)
        {
            _unitOfWork.CurrentSession.SaveOrUpdate(entity);
        }

        public TEntity Load<TEntity>(TId id) where TEntity : class
        {
            return _unitOfWork.CurrentSession.Load<TEntity>(id);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return _unitOfWork.CurrentSession.Linq<TEntity>();
        }

        public IQueryable<TEntity> Query<TEntity>(IDomainQuery<TEntity> query) where TEntity : class
        {
            return query.Apply(_unitOfWork.CurrentSession.Linq<TEntity>());
        }
    }
}