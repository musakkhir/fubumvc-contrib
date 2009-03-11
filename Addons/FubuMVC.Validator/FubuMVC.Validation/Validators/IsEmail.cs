using System.Text.RegularExpressions;

namespace FubuMVC.Validation.Validators
{
    public class IsEmail : IValidationRule
    {
        private const string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                                             + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                             + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                             + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                             + @"[a-zA-Z]{2,}))$";

        public bool Validate(object value)
        {
            return string.IsNullOrEmpty(value as string) || new Regex(patternStrict).IsMatch(value.ToString());
        }
    }
}