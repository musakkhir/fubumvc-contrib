using System.Web.UI;
using FubuMVC.Core.View;

namespace Fohjin.Core.Web.WebForms
{
    public class FohjinPage<MODEL> : Page, IFohjinPage, IFubuView<MODEL> 
        where MODEL : ViewModel
    {
        public void SetModel(object model)
        {
            Model = (MODEL) model;
        }

        public ViewModel GetModel()
        {
            return Model;
        }

        public MODEL Model{ get; set; }

        object IFohjinPage.Model { get{ return Model; } }
    }
}