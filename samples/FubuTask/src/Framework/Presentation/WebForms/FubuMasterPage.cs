using System.Web.UI;
using FubuMVC.Core.View;

namespace FubuMVC.Framework.Presentation.WebForms
{
    public class FubuMasterPage<TViewModelSupertype> : MasterPage, IFubuView<TViewModelSupertype> 
        where TViewModelSupertype : class
    {
        public TViewModelSupertype Model { get { return ((IFubuViewPage)Page).Model as TViewModelSupertype; } }
        
        public void SetModel(object model)
        {
            throw new System.NotImplementedException();
        }
    }
}