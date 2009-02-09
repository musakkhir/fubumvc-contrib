using System.Web.UI;

namespace Fohjin.Core.Web.WebForms
{
    public class FohjinMasterPage : MasterPage, IFohjinPage
    {
        object IFohjinPage.Model{ get { return ((IFohjinPage) Page).Model; } }

        public ViewModel Model { get { return ((IFohjinPage) Page).Model as ViewModel; } }
        
        public void SetModel(object model)
        {
            throw new System.NotImplementedException();
        }
    }
}