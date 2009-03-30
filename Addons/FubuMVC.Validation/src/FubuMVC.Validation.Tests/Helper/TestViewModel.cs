using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Tests.Helper
{
    public class TestViewModel : ICanBeValidated
    {
        public TestViewModel()
        {
            Empty_String = "";
            Null_String = null;
            Filled_String = "Something";
            Valid_Email = "jon@torresdal.net";
            False_Email_1 = "Mark.Nijhof";
            False_Email_2 = "Mark.Nijhof@nijhof.moc";
            Valid_Url_1 = "blog.fohjin.com";
            Valid_Url_2 = "http://blog.fohjin.com";
            False_Url = "http://test";
            Valid_Int = 99;
            False_Int = 100;
            MinValue_Int = int.MinValue;

            ValidationResults = new ValidationResults();
        }

        public string Valid_Email { get; set; }
        public string False_Email_1 { get; set; }
        public string False_Email_2 { get; set; }
        public string Filled_String { get; set; }
        public string Empty_String { get; set; }
        public string Valid_Url_1 { get; set; }
        public string Valid_Url_2 { get; set; }
        public string False_Url { get; set; }
        public string Null_String { get; set; }
        public int Valid_Int { get; set; }
        public int False_Int { get; set; }
        public int MinValue_Int { get; set; }

        public IValidationResults ValidationResults { get; private set; }
    }
}