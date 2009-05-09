using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.Dsl
{
    [TestFixture]
    public class ValidationDslSyntaxExample
    {
        [Test]
        public void A_complete_example_off_how_the_configuration_may_look_like()
        {
            ValidationConfig.Configure = x =>
            {
                x.ByDefault
                    .PropertiesMatching(p => !p.Name.Contains("Optional"), r =>
                        r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>()
                    )
                    .PropertiesMatching(p => p.Name.Contains("Email"), r =>
                        r.WillBeValidatedBy<IsEmail<CanBeAnyViewModel>>()
                    )
                    .PropertiesMatching(p => p.Name.Contains("Url"), r =>
                        r.WillBeValidatedBy<IsUrl<CanBeAnyViewModel>>()
                    )
                    .PropertiesMatching(p => p.Name.Contains("Answer"), r => 
                    {
                        r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>();
                        r.WillBeValidatedBy<IsUrl<CanBeAnyViewModel>>(p => 
                            p.NeedsAdditionalPropertyMatching(n => n.Name.Contains("Question")));
                    });

                x.AddViewModelsFromAssembly
                    .ContainingType<TestViewModel>()
                    .Where(t => t.Namespace.EndsWith("Tests.Helper") &&
                                t.ImplementsICanBeValidated());

                x.OverrideConfigFor<TestViewModel>()
                    .Property(p => p.Valid_Email, r =>
                    {
                        r.ClearValidationRules();
                        r.WillBeValidatedBy<IsEmail<TestViewModel>>();
                    }
                );
            };
        }
    }
}