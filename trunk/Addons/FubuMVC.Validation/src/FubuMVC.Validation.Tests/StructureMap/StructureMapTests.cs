using System;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.StructureMap
{
    [TestFixture]
    public class StructureMapTests
    {
        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        [Ignore("Will fail if the second test runs first...")]
        public void Should_not_be_able_to_instatiate_ValidationConfig_without_configuring_the_framework()
        {
            new ValidationConfig();
        }

        [Test]
        public void Should_be_able_to_instatiate_ValidationConfig_after_configuring_the_framework()
        {
            ValidationConfig.Configure = x =>
            {
                x.AddViewModelsFromAssembly
                    .ContainingType<TestViewModel>()
                    .Where(w => true);
            };

            new ValidationConfig();
        }
    }
}