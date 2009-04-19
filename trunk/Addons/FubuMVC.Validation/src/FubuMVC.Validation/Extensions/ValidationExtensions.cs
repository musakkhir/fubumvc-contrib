using System;
using System.Linq.Expressions;
using FubuMVC.Core.View;

namespace FubuMVC.Validation.Extensions
{
    public static class ValidationExtensions
    {
        public static ValidationExpression<TViewModel> Validate<TViewModel>(this IFubuView<TViewModel> viewModel, Expression<Func<TViewModel, object>> propertySelector) where TViewModel : class
        {
            return new ValidationExpression<TViewModel>(viewModel, propertySelector);
        }

        public static SimpleValidationExpression<TViewModel> Validate<TViewModel>(this IFubuView<TViewModel> viewModel) where TViewModel : class
        {
            return new SimpleValidationExpression<TViewModel>(viewModel);
        }
    }
}