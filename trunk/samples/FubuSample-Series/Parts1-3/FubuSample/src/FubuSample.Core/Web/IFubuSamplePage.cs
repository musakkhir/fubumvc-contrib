using FubuMVC.Core.View;

namespace FubuSample.Core.Web
{
    public interface IFubuSamplePage : IFubuViewWithModel
    {
        object Model { get; }
    }
}