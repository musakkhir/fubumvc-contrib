using System;

namespace Fohjin.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Initialize();
        void Commit();
        void Rollback();
    }
}