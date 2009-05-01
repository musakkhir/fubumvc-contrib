using System;
using Fohjin.Core.Domain;
using NUnit.Framework;

namespace Fohjin.IntegrationTests.Domain_Persistence
{
    [TestFixture]
    public class TagPersistenceTester : PersistenceTesterContext<Tag>
    {
        [Test]
        public void should_load_and_save_a_tag()
        {
            Specification
                .CheckProperty(t => t.Name, "name, anything here")
                .CheckProperty(u => u.CreatedDate, DateTime.Parse("2008-12-01"))
                .VerifyTheMappings();
        }
    }
}