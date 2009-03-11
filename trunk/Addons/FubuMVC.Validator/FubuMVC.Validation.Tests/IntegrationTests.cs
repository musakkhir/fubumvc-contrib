using FubuMVC.Validation.Tests.ViewModels;
using FubuMVC.Validation.Validators;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void Should_be_able_to_do_a_complete_configuration()
        {
            ValidationConfig.Configure = x =>
            {
                x.ByDefault.EveryViewModel(convention => convention
                    .Property(property => !property.Name.EndsWith("Optional"))
                    .WillBeValidatedBy<IsRequired>());

                x.ByDefault.EveryViewModel(convention => convention
                    .Property(property => property.Name.StartsWith("Email"))
                    .WillBeValidatedBy<IsRequired>()
                    .WillBeValidatedBy<IsEmail>());

                x.ByDefault.EveryViewModel(convention => convention
                    .Property(property => property.Name.Contains("Url"))
                    .WillBeValidatedBy<IsUrl>());

                x.AddViewModelsFromAssembly
                    .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                        t.Namespace.EndsWith("Tests.ViewModels")));

                x.OverrideConfigFor<ValidationTestSUT1>(convention => convention
                    .RemoveConvention(property => property.Name.StartsWith("Email")));

                x.OverrideConfigFor<ValidationTestSUT2>(convention => convention
                    .ForConvention(property => property.Name.StartsWith("Url"))
                    .AddRule<IsRequired>());

                // I forgot RemoveRule :)
                //x.OverrideConfigFor<ValidationTestSUT2>(convention => convention
                //    .ForConvention(property => property.Name.StartsWith("Url"))
                //    .RemoveRule<IsRequired>());
            };
        }
    }
}