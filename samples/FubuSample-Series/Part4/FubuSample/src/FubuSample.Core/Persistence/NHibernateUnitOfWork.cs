using System;
using FluentNHibernate.Framework;
using NHibernate;

namespace FubuSample.Core.Persistence
{
    public class NHibernateUnitOfWork : INHibernateUnitOfWork
    {
        private ITransaction _transaction;
        private bool _isDisposed;
        private readonly ISessionSource _source;
        private bool _isInitialized;

        public NHibernateUnitOfWork(ISessionSource source)
        {
            _source = source;
        }

        public void Initialize()
        {
            should_not_currently_be_disposed();

            CurrentSession = _source.CreateSession();
            begin_new_transaction();

            _isInitialized = true;
        }

        public ISession CurrentSession { get; private set; }

        public void Commit()
        {
            should_not_currently_be_disposed();
            should_be_initialized_first();

            _transaction.Commit();

            begin_new_transaction();
        }

        private void begin_new_transaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            _transaction = CurrentSession.BeginTransaction();
        }

        public void Rollback()
        {
            should_not_currently_be_disposed();
            should_be_initialized_first();

            _transaction.Rollback();

            begin_new_transaction();
        }

        private void should_not_currently_be_disposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }

        private void should_be_initialized_first()
        {
            if (!_isInitialized) throw new InvalidOperationException("Must initialize (call Initialize()) on NHibernateUnitOfWork before commiting or rolling back");
        }

        public void Dispose()
        {
            if (_isDisposed || !_isInitialized) return;

            _transaction.Dispose();
            CurrentSession.Dispose();

            _isDisposed = true;
        }
    }
}