using FubuMVC.Framework.NHibernate.Config;
using FubuMVC.Tests;
using FubuTask.Core.Domain;
using FubuTask.Core.Domain.Deep;
using NUnit.Framework;

namespace FubuTask.Tests.Core.Persistence
{
    [TestFixture]
    public class when_determining_whether_a_type_should_be_mapped_as_an_entity_the_alteration
    {
        [Test]
        public void should_only_map_subclasses_of_entity()
        {
            AutoMapAlteration<TestEntityBase>.IsMappable(typeof (DummyClass)).ShouldBeFalse();
        }

        [Test]
        public void should_only_map_types_in_the_domain_namespace()
        {
            AutoMapAlteration<TestEntityBase>.IsMappable(typeof(DummyEntityClass)).ShouldBeFalse();
        }

        [Test]
        public void should_map_subclasses_in_the_same_namespace_as_entity()
        {
            AutoMapAlteration<TestEntityBase>.IsMappable(typeof(TestEntityForMapping)).ShouldBeTrue();
        }

        [Test]
        public void should_map_subclasses_in_namespaces_under_entity()
        {
            AutoMapAlteration<TestEntityBase>.IsMappable(typeof(TestDeepEntityForMapping)).ShouldBeTrue();
        }

        public class DummyClass{}

        public class DummyEntityClass : TestEntityBase { }
    }
}

namespace FubuTask.Core.Domain
{
    public class TestEntityBase{}

    public class TestEntityForMapping : TestEntityBase { }

    namespace Deep
    {
        public class TestDeepEntityForMapping : TestEntityBase { }
    }
}