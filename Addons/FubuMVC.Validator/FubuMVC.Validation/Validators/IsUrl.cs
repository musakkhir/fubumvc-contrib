using System.Text.RegularExpressions;

namespace FubuMVC.Validation.Validators
{
    public class IsUrl : IValidationRule
    {
        private const string regexPattern = @"^(https?://)" 
                                            + @"?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@ 
                                            + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184 
                                            + @"|" // allows either IP or domain 
                                            + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www. 
                                            + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // second level domain 
                                            + @"[a-z]{2,6})" // first level domain- .com or .museum 
                                            + @"(:[0-9]{1,4})?" // port number- :80 
                                            + @"((/?)|" // a slash isn't required if there is no file name 
                                            + @"(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";

        public bool Validate(object value)
        {
            return string.IsNullOrEmpty(value as string) || new Regex(regexPattern, RegexOptions.ExplicitCapture).IsMatch(value.ToString());
        }
    }
}