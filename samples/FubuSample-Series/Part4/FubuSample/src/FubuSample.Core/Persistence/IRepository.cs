using System;
using System.Linq;
using FubuSample.Core.Domain;

namespace FubuSample.Core.Persistence
{
    public interface IRepository
    {
        void Save<ENTITY>(ENTITY entity)
            where ENTITY : DomainEntity;

        ENTITY Load<ENTITY>(Guid id)
            where ENTITY : DomainEntity;

        IQueryable<ENTITY> Query<ENTITY>()
            where ENTITY : DomainEntity;

        IQueryable<ENTITY> Query<ENTITY>(IDomainQuery<ENTITY> whereQuery)
            where ENTITY : DomainEntity;

        void Delete<ENTITY>(ENTITY entity);

        void DeleteAll<ENTITY>();
    }
}