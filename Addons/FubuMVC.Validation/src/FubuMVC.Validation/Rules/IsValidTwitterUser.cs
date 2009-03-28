using System;
using System.Linq.Expressions;
using System.Net;
using FubuMVC.Core;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Rules
{
    public class IsValidTwitterUser<TViewModel> : IValidationRule<TViewModel>
    {
        private const string _twitterUrl = "http://twitter.com/{0}";

        private readonly Expression<Func<TViewModel, string>> _propertyExpression;

        public IsValidTwitterUser(Expression<Func<TViewModel, string>> propertyExpression)
        {
            _propertyExpression = propertyExpression;
            PropertyFilter = new UglyExpressionConvertor().ToString(_propertyExpression);
        }

        public bool IsValid(TViewModel viewModel)
        {
            var twitterUserName = _propertyExpression.Compile().Invoke(viewModel);

            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_twitterUrl.ToFormat(twitterUserName));
                response = (HttpWebResponse)request.GetResponse();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch (TimeoutException)
            {
                return true; // Giving the benefit of the doubt
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        public string PropertyFilter { get; private set; }
    }
}