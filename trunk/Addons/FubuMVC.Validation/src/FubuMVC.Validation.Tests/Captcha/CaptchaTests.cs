using FubuMVC.Tests;
using FubuMVC.Validation.Captcha;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FubuMVC.Validation.Tests.Captcha
{
    [TestFixture]
    public class CaptchaTests
    {
        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_sum_results_in_valid()
        {
            const string captcha = "2 + 2";
            const string answer = "4";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_sum_results_in_invalid()
        {
            const string captcha = "2 + 2";
            const string answer = "2";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_sum_results_also_in_invalid()
        {
            const string captcha = "2 + 2";
            const string answer = "a";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_min_results_in_valid()
        {
            const string captcha = "1 - 2";
            const string answer = "-1";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_min_results_in_invalid()
        {
            const string captcha = "2 - 1";
            const string answer = "0";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_times_results_in_valid()
        {
            const string captcha = "2 * 3";
            const string answer = "6";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_times_results_in_invalid()
        {
            const string captcha = "2 * 3";
            const string answer = "4";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_devide_results_in_valid()
        {
            const string captcha = "6 / 2";
            const string answer = "3";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_devide_results_in_invalid()
        {
            const string captcha = "6 / 1";
            const string answer = "0";
            const string question = captcha + " == " + answer;

            new CaptchaEvaluator().IsValid(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_not_generate_a_captcha_question_when_not_configured()
        {
            new CaptchaGeneator().ToString().ShouldEqual(string.Empty);
        }

        [Test]
        public void Should_be_able_to_generate_a_add_captcha_question()
        {
            var question = new CaptchaGeneator()
                .ConfigureToUse(CaptchaOpperator.Add);
            question.ToString().ShouldContain(" + ");
            question.ToString().Length.ShouldEqual(5);
        }

        [Test]
        public void Should_be_able_to_generate_a_subtract_captcha_question()
        {
            var question = new CaptchaGeneator()
                .ConfigureToUse(CaptchaOpperator.Subtract);
            question.ToString().ShouldContain(" - ");
            question.ToString().Length.ShouldEqual(5);
        }

        [Test]
        public void Should_be_able_to_generate_a_multiply_captcha_question()
        {
            var question = new CaptchaGeneator()
                .ConfigureToUse(CaptchaOpperator.Multiply);
            question.ToString().ShouldContain(" * ");
            question.ToString().Length.ShouldEqual(5);
        }

        [Test]
        public void Should_be_able_to_generate_a_divide_captcha_question()
        {
            var question = new CaptchaGeneator()
                .ConfigureToUse(CaptchaOpperator.Divide);
            question.ToString().ShouldContain(" / ");
            question.ToString().Length.ShouldEqual(5);
        }

        [Test]
        public void Should_be_able_to_generate_a_add_or_substract_captcha_question()
        {
            var question = new CaptchaGeneator()
                .ConfigureToUse(CaptchaOpperator.Add)
                .ConfigureToUse(CaptchaOpperator.Subtract);

            Assert.That((question.ToString().Contains(" + ") || question.ToString().Contains(" - ")), Is.True);

            question.ToString().Length.ShouldEqual(5);
        }
    }
}