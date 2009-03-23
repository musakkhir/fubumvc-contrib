using FubuMVC.Tests;
using FubuMVC.Validation.Captcha;
using NUnit.Framework;

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

            new Evaluator().Validate(question).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_sum_results_in_invalid()
        {
            const string captcha = "2 + 2";
            const string answer = "2";
            const string question = captcha + " == " + answer;

            new Evaluator().Validate(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_sum_results_also_in_invalid()
        {
            const string captcha = "2 + 2";
            const string answer = "a";
            const string question = captcha + " == " + answer;

            new Evaluator().Validate(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_min_results_in_valid()
        {
            const string captcha = "1 - 2";
            const string answer = "-1";
            const string question = captcha + " == " + answer;

            new Evaluator().Validate(question).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_min_results_in_invalid()
        {
            const string captcha = "2 - 1";
            const string answer = "0";
            const string question = captcha + " == " + answer;

            new Evaluator().Validate(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_times_results_in_valid()
        {
            const string captcha = "2 * 3";
            const string answer = "6";
            const string question = captcha + " == " + answer;

            new Evaluator().Validate(question).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_times_results_in_invalid()
        {
            const string captcha = "2 * 3";
            const string answer = "4";
            const string question = captcha + " == " + answer;

            new Evaluator().Validate(question).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_devide_results_in_valid()
        {
            const string captcha = "6 / 2";
            const string answer = "3";
            const string question = captcha + " == " + answer;

            new Evaluator().Validate(question).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_calculate_a_captcha_calculation_from_a_single_string_devide_results_in_invalid()
        {
            const string captcha = "6 / 1";
            const string answer = "0";
            const string question = captcha + " == " + answer;

            new Evaluator().Validate(question).ShouldBeFalse();
        }
    }
}