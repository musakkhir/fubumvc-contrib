using System.Web.UI;
using FubuMVC.Core.View;

namespace FubuMVC.Framework.Presentation.WebForms
{
    public class FubuPage<TViewModel> : Page, IFubuView<TViewModel>, IFubuViewPage
        where TViewModel : class
    {
        public void SetModel(object model)
        {
            Model = (TViewModel)model;
        }

        public TViewModel GetModel()
        {
            return Model;
        }

        public TViewModel Model { get; set; }
        object IFubuViewPage.Model { get{ return Model; } }
    }
}