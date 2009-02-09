using System.Web.UI;
using FubuMVC.Core.View;

namespace Fohjin.Core.Web.WebForms
{
    public class FohjinUserControl<MODEL> : UserControl, IFohjinPage, IFubuView<MODEL> 
        where MODEL : class
    {
        public void SetModel(object model)
        {
            Model = (MODEL) model;
        }

        public object GetModel()
        {
            return Model;
        }

        public MODEL Model{ get; set; }

        object IFohjinPage.Model { get { return Model; } }
    }
}