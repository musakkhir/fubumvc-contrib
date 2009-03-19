using FubuMVC.Core.View;

namespace FubuMVC.Validation.Tests.Helper
{
    public class TestView : IFubuView<TestViewModel> 
    {
        public void SetModel(object model)
        {
            Model = model as TestViewModel;
        }

        public TestViewModel Model { get; private set; }
    }
}