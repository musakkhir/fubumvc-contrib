using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.Dsl
{
    [TestFixture]
    public class ValidationMapTests
    {
        [Test]
        [Ignore("Work in progress")]
        public void Should_be_able_to_define_rules_for_a_specific_class_using_the_ValidationMap()
        {
            Assert.Fail();
            var validationMap = new ValidationMap<TestEntity>();
            validationMap.Property(x => x.Email, rule => 
                rule.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>());
        }
    }
}