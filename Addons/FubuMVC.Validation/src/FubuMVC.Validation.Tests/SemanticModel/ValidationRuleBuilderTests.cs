using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuMVC.Tests;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.SemanticModel;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.SemanticModel
{
    [TestFixture]
    public class ValidationRuleBuilderTests
    {
        private ValidationRuleBuilder _validationRuleBuilder;
        private Type _discoveredType;
        private PropertyInfo _propertyInfoOfStringProperty;
        private PropertyInfo _propertyInfoOfIntProperty;

        [SetUp]
        public void SetUp()
        {
            _validationRuleBuilder = new ValidationRuleBuilder();
            _discoveredType = typeof(TestViewModel);
            _propertyInfoOfStringProperty = typeof(TestViewModel).GetProperty("Valid_Email");
            _propertyInfoOfIntProperty = typeof(TestViewModel).GetProperty("Valid_Int");
        }

        [Test]
        public void Should_be_able_to_build_the_IsRequired_rule()
        {
            var ruleType = typeof(IsRequired<>);
            var list = new List<object>();

            _validationRuleBuilder.Build<TestViewModel>(_discoveredType, ruleType, _propertyInfoOfStringProperty, new List<PropertyInfo>(), list.Add);

            list.Count.ShouldEqual(1);
            list.First().GetType().ShouldEqual(typeof(IsRequired<TestViewModel>));
        }

        [Test]
        public void Should_be_able_to_build_the_IsEmail_rule()
        {
            var ruleType = typeof(IsEmail<>);
            var list = new List<object>();

            _validationRuleBuilder.Build<TestViewModel>(_discoveredType, ruleType, _propertyInfoOfStringProperty, new List<PropertyInfo>(), list.Add);

            list.Count.ShouldEqual(1);
            list.First().GetType().ShouldEqual(typeof(IsEmail<TestViewModel>));
        }

        [Test]
        public void Should_be_able_to_build_the_IsUrl_rule()
        {
            var ruleType = typeof(IsUrl<>);
            var list = new List<object>();

            _validationRuleBuilder.Build<TestViewModel>(_discoveredType, ruleType, _propertyInfoOfStringProperty, new List<PropertyInfo>(), list.Add);

            list.Count.ShouldEqual(1);
            list.First().GetType().ShouldEqual(typeof(IsUrl<TestViewModel>));
        }

        [Test]
        public void Should_be_able_to_build_the_IsNumberBelow100_rule()
        {
            var ruleType = typeof(IsNumberBelow100<>);
            var list = new List<object>();

            _validationRuleBuilder.Build<TestViewModel>(_discoveredType, ruleType, _propertyInfoOfIntProperty, new List<PropertyInfo>(), list.Add);

            list.Count.ShouldEqual(1);
            list.First().GetType().ShouldEqual(typeof(IsNumberBelow100<TestViewModel>));
        }

        [Test]
        public void Should_be_able_to_build_the_IsValidCaptcha_rule()
        {
            var ruleType = typeof(IsValidCaptcha<>);
            var list = new List<object>();
            var properties = new List<PropertyInfo> { _propertyInfoOfStringProperty };

            _validationRuleBuilder.Build<TestViewModel>(_discoveredType, ruleType, _propertyInfoOfStringProperty, properties, list.Add);

            list.Count.ShouldEqual(1);
            list.First().GetType().ShouldEqual(typeof(IsValidCaptcha<TestViewModel>));
        }
    }
}