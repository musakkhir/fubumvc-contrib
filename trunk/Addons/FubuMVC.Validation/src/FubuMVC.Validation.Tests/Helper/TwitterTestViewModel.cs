using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Tests.Helper
{
    public class TwitterTestViewModel : ICanBeValidated<TwitterTestViewModel>
    {
        public TwitterTestViewModel()
        {
            ValidationResults = new ValidationResults<TwitterTestViewModel>();

            Good_TwitterUser = "MarkNijhof";
            Bad_TwitterUser = "Mark______Nijhof"; // I hope this stays a none existing user on Twitter :)
        }

        public string Good_TwitterUser { get; set; }
        public string Bad_TwitterUser { get; set; }

        public IValidationResults<TwitterTestViewModel> ValidationResults { get; private set; }
    }
}