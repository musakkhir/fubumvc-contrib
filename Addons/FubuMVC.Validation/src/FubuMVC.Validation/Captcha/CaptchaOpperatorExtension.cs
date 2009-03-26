namespace FubuMVC.Validation.Captcha
{
    public static class CaptchaOpperatorExtension
    {
        public static string AsString(this CaptchaOpperator opperator)
        {
            switch (opperator)
            {
                case CaptchaOpperator.Add:
                    return "+";
                case CaptchaOpperator.Subtract:
                    return "-";
                case CaptchaOpperator.Multiply:
                    return "*";
                case CaptchaOpperator.Divide:
                    return "/";
            }
            return string.Empty;
        }
    }
}