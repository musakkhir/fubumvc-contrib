using FubuMVC.Core.View;

namespace Fohjin.Core.Web
{
    public interface IFohjinPage : IFubuViewWithModel
    {
        object Model{ get; }
    }
}