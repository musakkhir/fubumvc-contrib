using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Tests.Helper
{
    public class ValidTestViewModel : ICanBeValidated
    {
        public ValidTestViewModel()
        {
            Filled_String = "Something";
            Valid_Email = "Mark.Nijof@Gmail.com";
            Valid_Url_1 = "blog.fohjin.com";
            Valid_Url_2 = "http://blog.fohjin.com";

            ValidationResults = new ValidationResults();
        }

        public string Valid_Email { get; set; }
        public string Filled_String { get; set; }
        public string Valid_Url_1 { get; set; }
        public string Valid_Url_2 { get; set; }

        public IValidationResults ValidationResults { get; private set; }
    }
}