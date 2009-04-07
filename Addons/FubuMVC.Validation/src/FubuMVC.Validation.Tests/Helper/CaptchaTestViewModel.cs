using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Tests.Helper
{
    public class CaptchaTestViewModel : ICanBeValidated<CaptchaTestViewModel>
    {
        public CaptchaTestViewModel()
        {
            ValidationResults = new ValidationResults<CaptchaTestViewModel>();

            Question = "2 + 4";
            Good_Answer = "6";
            Bad_Answer = "4";
        }

        public string Question { get; set; }
        public string Good_Answer { get; set; }
        public string Bad_Answer { get; set; }

        public IValidationResults<CaptchaTestViewModel> ValidationResults { get; private set; }
    }
}