using System;
using System.Linq;
using FubuMVC.Framework.NHibernate;
using FubuMVC.Tests;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Framework.Tests.Persistence.NHibernate
{
    [TestFixture]
    public class NHibernateRepositoryTester
    {
        private INHibernateUnitOfWork _uow;
        private NHibernateRepository<Guid> _repo;
        private ISession _session;

        [SetUp]
        public void SetUp()
        {
            _uow = MockRepository.GenerateMock<INHibernateUnitOfWork>();
            _session = MockRepository.GenerateMock<ISession>();
            _uow.Stub(u => u.CurrentSession).Return(_session);
            _repo = new NHibernateRepository<Guid>(_uow);
        }

        [Test]
        public void save_should_save_on_the_session()
        {
            var entity = new DummyDomainEntity();
            _repo.Save(entity);

            _session.AssertWasCalled(s => s.SaveOrUpdate(entity));
        }

        [Test]
        public void load_should_load_from_the_session()
        {
            var entityId = Guid.NewGuid();
            _repo.Load<DummyDomainEntity>(entityId);

            _session.AssertWasCalled(s => s.Load<DummyDomainEntity>(entityId));
        }

        [Test]
        public void query_should_start_a_linq_query_on_the_session()
        {
            _repo.Query<DummyDomainEntity>().ShouldBeOfType<IQueryable<DummyDomainEntity>>();
        }

        public class DummyDomainEntity
        {
            
        }
    }
}