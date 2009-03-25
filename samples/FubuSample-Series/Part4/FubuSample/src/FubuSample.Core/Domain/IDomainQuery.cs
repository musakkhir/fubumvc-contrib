using System;
using System.Linq.Expressions;

namespace FubuSample.Core.Domain
{
    public interface IDomainQuery<ENTITY>
        where ENTITY : DomainEntity
    {
        Expression<Func<ENTITY, bool>> Expression { get; }
    }
}