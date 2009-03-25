using System;
using System.Linq;
using FubuSample.Core.Domain;
using NHibernate.Linq;

namespace FubuSample.Core.Persistence
{
    public class NHibernateRepository : IRepository
    {
        private readonly INHibernateUnitOfWork _unitOfWork;

        public NHibernateRepository(INHibernateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Save<ENTITY>(ENTITY entity) where ENTITY : DomainEntity
        {
            _unitOfWork.CurrentSession.SaveOrUpdate(entity);
        }

        public ENTITY Load<ENTITY>(Guid id) where ENTITY : DomainEntity
        {
            return _unitOfWork.CurrentSession.Load<ENTITY>(id);
        }

        public IQueryable<ENTITY> Query<ENTITY>() where ENTITY : DomainEntity
        {
            return _unitOfWork.CurrentSession.Linq<ENTITY>();
        }

        public IQueryable<ENTITY> Query<ENTITY>(IDomainQuery<ENTITY> whereQuery) where ENTITY : DomainEntity
        {
            return _unitOfWork.CurrentSession.Linq<ENTITY>().Where(whereQuery.Expression);
        }

        public void Delete<ENTITY>(ENTITY entity)
        {
            _unitOfWork.CurrentSession.Delete(entity);
        }

        public void DeleteAll<ENTITY>()
        {
            var query = String.Format("from {0}", typeof(ENTITY).Name);
            _unitOfWork.CurrentSession.Delete(query);
        }
    }
}