using System.Web.UI;
using FubuMVC.Core.View;

namespace FubuSample.Core.Web.WebForms
{
    public class FubuSampleUserControl<MODEL> : UserControl, IFubuSamplePage, IFubuView<MODEL>
        where MODEL : class
    {
        public void SetModel(object model)
        {
            Model = (MODEL)model;
        }

        public object GetModel()
        {
            return Model;
        }

        public MODEL Model { get; set; }

        object IFubuSamplePage.Model { get { return Model; } }
    }
}