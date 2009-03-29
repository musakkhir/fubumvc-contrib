using FubuMVC.Core;
using FubuMVC.Validation.Behaviors;
using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.SemanticModel
{
    public class ValidationConfiguration : IValidate
    {
        public ValidationConfiguration()
        {
            DefaultPropertyConventions = new DefaultPropertyConventions();
            DiscoveredTypes = new DiscoveredTypes(DefaultPropertyConventions);
        }

        public DefaultPropertyConventions DefaultPropertyConventions { get; private set; }
        public DiscoveredTypes DiscoveredTypes { get; private set; }

        public void Validate<TViewModel>(TViewModel viewModel) where TViewModel : ICanBeValidated
        {
            DiscoveredTypes.GetRulesFor(viewModel).Each(rule =>
            {
                if (!rule.IsValid(viewModel))
                    viewModel.ValidationResults.AddInvalidField(rule.PropertyFilter, rule.GetType());
            });
        }
    }
}