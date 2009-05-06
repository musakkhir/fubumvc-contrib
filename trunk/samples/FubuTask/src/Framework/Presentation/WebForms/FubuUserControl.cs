using System.Web.UI;
using FubuMVC.Core.View;

namespace FubuMVC.Framework.Presentation.WebForms
{
    public class FubuUserControl<TViewModel> : UserControl, IFubuView<TViewModel> 
        where TViewModel : class
    {
        public void SetModel(object model)
        {
            Model = (TViewModel) model;
        }

        public object GetModel()
        {
            return Model;
        }

        public TViewModel Model{ get; set; }
    }
}