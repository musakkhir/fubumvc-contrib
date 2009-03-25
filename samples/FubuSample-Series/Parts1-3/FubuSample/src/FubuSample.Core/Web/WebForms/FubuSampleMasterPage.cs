using System.Web.UI;

namespace FubuSample.Core.Web.WebForms
{
    public class FubuSampleMasterPage : MasterPage, IFubuSamplePage
    {
        object IFubuSamplePage.Model { get { return ((IFubuSamplePage) Page).Model; } }

        public ViewModel Model { get { return ((IFubuSamplePage) Page).Model as ViewModel; } }

        public void SetModel(object model)
        {
            throw new System.NotImplementedException();
        }
    }
}