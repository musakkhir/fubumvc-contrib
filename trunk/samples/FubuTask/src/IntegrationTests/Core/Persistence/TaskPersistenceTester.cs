using System;
using System.IO;
using FubuMVC.Framework.Tests.Persistence;
using FubuTask.Config;
using FubuTask.Core.Domain;
using NUnit.Framework;
using StructureMap;

namespace FubuTask.IntegrationTests.Core.Persistence
{
    [TestFixture]
    public class TaskPersistenceTester
    {
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetFullPath(@"..\..\..\Web\App_Data"));
            ObjectFactory.Initialize(x => x.AddRegistry(new PersistenceRegistry()));
        }

        [Test]
        public void Save_and_load_a_Task()
        {
            new PersistenceSpecification<Task, Guid>(t=>t.Id)
                .CheckProperty(s => s.Title, "foo")
                .VerifyTheMappings();
        }
    }
}