using System.Web.UI;
using FubuMVC.Core.View;

namespace FubuSample.Core.Web.WebForms
{
    public class FubuSamplePage<MODEL> : Page, IFubuSamplePage, IFubuView<MODEL>
        where MODEL : ViewModel
    {
        public void SetModel(object model)
        {
            Model = (MODEL)model;
        }

        public ViewModel GetModel()
        {
            return Model;
        }

        public MODEL Model { get; set; }

        object IFubuSamplePage.Model { get { return Model; } }
    }
}