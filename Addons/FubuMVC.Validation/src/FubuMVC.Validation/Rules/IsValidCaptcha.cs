using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuMVC.Core;
using FubuMVC.Validation.Captcha;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Rules
{
    public class IsValidCaptcha<TViewModel> : IValidationRule<TViewModel>
    {
        private readonly Expression<Func<TViewModel, string>> _questionPropertyExpression;
        private readonly Expression<Func<TViewModel, string>> _answerPropertyExpression;
        private readonly CaptchaEvaluator _captchaEvaluator;

        public IsValidCaptcha(Expression<Func<TViewModel, string>> questionPropertyExpression, Expression<Func<TViewModel, string>> answerPropertyExpression)
        {
            ConstructorArguments = new List<object> { questionPropertyExpression, answerPropertyExpression };
            _questionPropertyExpression = questionPropertyExpression;
            _answerPropertyExpression = answerPropertyExpression;
            PropertyFilter = new UglyExpressionConvertor().ToString(_questionPropertyExpression);
            _captchaEvaluator = new CaptchaEvaluator();
        }

        public bool IsValid(TViewModel viewModel)
        {
            var question = _questionPropertyExpression.Compile().Invoke(viewModel);
            var answer = _answerPropertyExpression.Compile().Invoke(viewModel);

            return _captchaEvaluator.IsValid("{0} == {1}".ToFormat(question, answer));
        }

        public string PropertyFilter { get; private set; }
        public IList<object> ConstructorArguments { get; private set; }
    }
}