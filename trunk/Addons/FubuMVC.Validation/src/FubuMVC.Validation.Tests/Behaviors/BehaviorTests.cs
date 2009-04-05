using FubuMVC.Validation.Behaviors;
using FubuMVC.Validation.Results;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Validation.Tests.Behaviors
{
    [TestFixture]
    public class BehaviorTests
    {
        [Test]
        public void Behavior_should_be_calling_validate_in_the_ValidationConfiguration()
        {
            var testViewModel = new TestViewModel();

            var mock = MockRepository.GenerateStub<IValidate>();
            mock.Expect(x => x.Validate(testViewModel)).Return(new ValidationResults());
            var sut = new validate_input_view_model_using_convention_based_validation_rules_server_side(mock);

            sut.PrepareInput(testViewModel);

            mock.VerifyAllExpectations();
        }

        [Test]
        public void Behavior_should_not_be_calling_validate_in_the_ValidationConfiguration()
        {
            var testViewModel = new TestViewModelNotImplementingICanBeValidated();

            var mock = MockRepository.GenerateStub<IValidate>();
            mock.Expect(x => x.Validate(testViewModel)).Return(new ValidationResults());
            var sut = new validate_input_view_model_using_convention_based_validation_rules_server_side(mock);

            sut.PrepareInput(testViewModel);

            mock.AssertWasNotCalled(x => x.Validate(new TestViewModel()), o => o.IgnoreArguments());
        }
    }
}