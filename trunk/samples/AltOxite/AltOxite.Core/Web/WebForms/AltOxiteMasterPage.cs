using System.Web.UI;
using FubuMVC.Core.View;

namespace AltOxite.Core.Web.WebForms
{
    public class AltOxiteMasterPage : MasterPage, IAltOxitePage, IFubuView<ViewModel>
    {
        object IAltOxitePage.Model{ get { return ((IAltOxitePage) Page).Model; } }

        public ViewModel Model { get { return ((IAltOxitePage) Page).Model as ViewModel; } }
        
        public void SetModel(object model)
        {
            throw new System.NotImplementedException();
        }
    }
}