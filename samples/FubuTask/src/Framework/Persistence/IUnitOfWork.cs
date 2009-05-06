using System;

namespace FubuMVC.Framework.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Initialize();
        void Commit();
        void Rollback();
    }
}