using System.Linq;
using FubuMVC.Tests;
using FubuMVC.Validation.DSL;
using FubuMVC.Validation.Tests.ViewModels;
using FubuMVC.Validation.Validators;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests
{
    [TestFixture]
    public class ValidationRuleTest
    {
        [Test]
        public void Should_be_able_to_run_a_required_validation_rule_and_pass()
        {
            var someObject = "some value";
            new IsRequired().Validate(someObject).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_required_validation_rule_and_fail()
        {
            var someObject = "";
            new IsRequired().Validate(someObject).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_run_a_is_email_validation_rule_and_pass()
        {
            var someObject = "mark.nijhof@gmail.com";
            new IsEmail().Validate(someObject).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_email_validation_rule_and_fail()
        {
            var someObject = "mark.nijhof.@gmail.com";
            new IsEmail().Validate(someObject).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_run_a_is_email_validation_rule_empty_and_pass()
        {
            var someObject = "";
            new IsEmail().Validate(someObject).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_and_pass()
        {
            var someObject = "blog.fohjin.com";
            new IsUrl().Validate(someObject).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_and_pass_2()
        {
            var someObject = "http://blog.fohjin.com";
            new IsUrl().Validate(someObject).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_and_fail()
        {
            var someObject = "http://test";
            new IsUrl().Validate(someObject).ShouldBeFalse();
        }

        [Test]
        public void Should_be_able_to_run_a_is_url_validation_rule_empty_and_pass()
        {
            var someObject = "";
            new IsUrl().Validate(someObject).ShouldBeTrue();
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsRequired_and_pass()
        {
            var validationTestSUT = new ValidationTestSUT { AnyString = "1234", Url = "1234", Email = "1234" };

            var propertyConvention = new PropertyConvention(property => !property.Name.EndsWith("Optional"));
            propertyConvention.AddRule<IsRequired>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(0);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsRequired_and_fail()
        {
            var validationTestSUT = new ValidationTestSUT { AnyString = "1234", Url = "1234" };

            var propertyConvention = new PropertyConvention(property => !property.Name.EndsWith("Optional"));
            propertyConvention.AddRule<IsRequired>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsEmail_and_pass()
        {
            var validationTestSUT = new ValidationTestSUT { Email = "mark.nijhof@gmail.com", EmailOptional = "mark@nijhof.com" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Email"));
            propertyConvention.AddRule<IsEmail>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(0);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsEmail_and_fail()
        {
            var validationTestSUT = new ValidationTestSUT { Email = "mark.nijhofgmail.com", EmailOptional = "" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Email"));
            propertyConvention.AddRule<IsEmail>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsUrl_and_pass()
        {
            var validationTestSUT = new ValidationTestSUT { Url = "www.nijhof.com", UrlOptional = "https://blog.fohjin.com/" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Url"));
            propertyConvention.AddRule<IsUrl>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(0);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsUrl_and_fail()
        {
            var validationTestSUT = new ValidationTestSUT { Url = "wwwnijhofcom", UrlOptional = "" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Url"));
            propertyConvention.AddRule<IsUrl>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsRequired_and_IsEmail_and_pass()
        {
            var validationTestSUT = new ValidationTestSUT { Email = "mark.nijhof@gmail.com", EmailOptional = "mark.nijhof@gmail.com" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Email"));
            propertyConvention.AddRule<IsRequired>();
            propertyConvention.AddRule<IsEmail>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(0);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsRequired_and_IsEmail_and_fail()
        {
            var validationTestSUT = new ValidationTestSUT { Email = "mark.nijhofgmail.com", EmailOptional = "" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Email"));
            propertyConvention.AddRule<IsRequired>();
            propertyConvention.AddRule<IsEmail>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(2);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsRequired_and_IsEmail_and_fail_2()
        {
            var validationTestSUT = new ValidationTestSUT { Email = "mark.nijhof@gmail.com", EmailOptional = "" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Email"));
            propertyConvention.AddRule<IsRequired>();
            propertyConvention.AddRule<IsEmail>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsRequired_and_IsEmail_and_fail_3()
        {
            var validationTestSUT = new ValidationTestSUT { Email = "mark.nijhof@gmail.com", EmailOptional = "mark.nijhofgmail.com" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Email"));
            propertyConvention.AddRule<IsRequired>();
            propertyConvention.AddRule<IsEmail>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetInvalidFields().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsRequired_and_IsEmail_and_return_IsEmail()
        {
            var validationTestSUT = new ValidationTestSUT { Email = "mark.nijhof@gmail.com", EmailOptional = "mark.nijhofgmail.com" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Email"));
            propertyConvention.AddRule<IsRequired>();
            propertyConvention.AddRule<IsEmail>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetBrokenRulesFor(validationTestSUT.GetInvalidFields().First()).First().ShouldEqual(typeof(IsEmail));
        }

        [Test]
        public void Should_be_able_to_add_default_validation_convention_with_IsRequired_and_IsEmail_and_return_IsRequired_and_IsEmail()
        {
            var validationTestSUT = new ValidationTestSUT { Email = "", EmailOptional = "mark.nijhofgmail.com" };

            var propertyConvention = new PropertyConvention(property => property.Name.Contains("Email"));
            propertyConvention.AddRule<IsRequired>();
            propertyConvention.AddRule<IsEmail>();

            var sut = new ViewModelPropertyRules();
            sut.AddConvention(propertyConvention);
            sut.Validate(validationTestSUT);

            validationTestSUT.GetBrokenRulesFor(validationTestSUT.GetInvalidFields().First()).First().ShouldEqual(typeof(IsRequired));
            validationTestSUT.GetBrokenRulesFor(validationTestSUT.GetInvalidFields().Last()).First().ShouldEqual(typeof(IsEmail));
        }
    }
}
