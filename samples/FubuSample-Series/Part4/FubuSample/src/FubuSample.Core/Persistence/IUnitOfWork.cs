using System;

namespace FubuSample.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Initialize();
        void Commit();
        void Rollback();
    }
}