using FubuMVC.Tests;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.Rules
{
    [TestFixture]
    public class ValidatorRuleTests
    {
        private TestViewModel _testViewModel;

        [SetUp]
        public void SetUp()
        {
            _testViewModel = new TestViewModel();
        }

        [Test]
        public void Should_return_string_representation_of_the_expression_provided_for_IsRequired()
        {
            new IsRequired<TestViewModel>(x => x.Filled_String)
                .PropertyFilter
                .ShouldEqual("property => property.Filled_String");
        }

        [Test]
        public void Should_be_able_to_run_a_required_validation_rule_and_pass()
        {
            new IsRequired<TestViewModel>(x => x.Filled_String)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_required_validation_rule_and_fail()
        {
            new IsRequired<TestViewModel>(x => x.Empty_String)
                .IsValid(_testViewModel)
                .ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_run_a_required_validation_rule_on_null_and_fail()
        {
            new IsRequired<TestViewModel>(x => x.Null_String)
                .IsValid(_testViewModel)
                .ShouldBeFalse();
        }

        [Test]
        public void Should_return_string_representation_of_the_expression_provided_for_IsEmail()
        {
            new IsEmail<TestViewModel>(x => x.Filled_String)
                .PropertyFilter
                .ShouldEqual("property => property.Filled_String");
        }

        [Test]
        public void Should_be_able_to_run_a_is_email_validation_rule_and_pass()
        {
            new IsEmail<TestViewModel>(x => x.Valid_Email)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_email_validation_rule_and_fail()
        {
            new IsEmail<TestViewModel>(x => x.False_Email_1)
                .IsValid(_testViewModel)
                .ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_run_a_is_email_validation_rule_and_fail_because_of_non_existing_domain()
        {
            new IsEmail<TestViewModel>(x => x.False_Email_2)
                .IsValid(_testViewModel)
                .ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_run_a_is_email_validation_rule_empty_and_pass()
        {
            new IsEmail<TestViewModel>(x => x.Empty_String)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_email_validation_rule_null_and_pass()
        {
            new IsEmail<TestViewModel>(x => x.Null_String)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_return_string_representation_of_the_expression_provided_for_IsUrl()
        {
            new IsUrl<TestViewModel>(x => x.Filled_String)
                .PropertyFilter
                .ShouldEqual("property => property.Filled_String");
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_and_pass()
        {
            new IsUrl<TestViewModel>(x => x.Valid_Url_1)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_and_pass_2()
        {
            new IsUrl<TestViewModel>(x => x.Valid_Url_2)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_and_fail()
        {
            new IsUrl<TestViewModel>(x => x.False_Url)
                .IsValid(_testViewModel)
                .ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_empty_and_pass()
        {
            new IsUrl<TestViewModel>(x => x.Empty_String)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_null_and_pass()
        {
            new IsUrl<TestViewModel>(x => x.Null_String)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_return_string_representation_of_the_expression_provided_for_IsNumberBelow100()
        {
            new IsNumberBelow100<TestViewModel>(x => x.Valid_Int)
                .PropertyFilter
                .ShouldEqual("property => property.Valid_Int");
        }

        [Test]
        public void Should_be_able_to_run_a_is_number_below_100_validation_rule_and_pass()
        {
            new IsNumberBelow100<TestViewModel>(x => x.Valid_Int)
                .IsValid(_testViewModel)
                .ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_number_below_100_validation_rule_and_fail()
        {
            new IsNumberBelow100<TestViewModel>(x => x.False_Int)
                .IsValid(_testViewModel)
                .ShouldBeFalse();
        }

        [Test]
        public void IsRequired_should_support_both_string_and_int()
        {
            new IsRequired<TestViewModel>(x => x.MinValue_Int)
                .IsValid(_testViewModel)
                .ShouldBeFalse();

            new IsRequired<TestViewModel>(x => x.Empty_String)
                .IsValid(_testViewModel)
                .ShouldBeFalse();
        }

        [Test]
        public void IsValidCaptcha_should_be_able_to_validate_a_question_and_good_answer()
        {
            new IsValidCaptcha<CaptchaTestViewModel>(x => x.Question, x => x.Good_Answer)
                .IsValid(new CaptchaTestViewModel())
                .ShouldBeTrue();
        }

        [Test]
        public void IsValidCaptcha_should_be_able_to_validate_a_question_and_bad_answer()
        {
            new IsValidCaptcha<CaptchaTestViewModel>(x => x.Question, x => x.Bad_Answer)
                .IsValid(new CaptchaTestViewModel())
                .ShouldBeFalse();
        }

        [Test]
        public void IsValidTwitterUser_should_be_able_to_validate_if_a_twitter_exists_which_does()
        {
            new IsValidTwitterUser<TwitterTestViewModel>(x => x.Good_TwitterUser)
                .IsValid(new TwitterTestViewModel())
                .ShouldBeTrue();
        }

        [Test]
        public void IsValidTwitterUser_should_be_able_to_validate_if_a_twitter_exists_which_does_not()
        {
            new IsValidTwitterUser<TwitterTestViewModel>(x => x.Bad_TwitterUser)
                .IsValid(new TwitterTestViewModel())
                .ShouldBeFalse();
        }
    }
}