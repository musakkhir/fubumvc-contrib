using System;

namespace Fohjin.Core.Web.Controllers
{
    public class AboutController
    {
        public AboutViewModel Index(AboutSetupViewModel inModel)
        {
            return new AboutViewModel();
        }
    }

    public class AboutSetupViewModel : ViewModel
    {
    }

    [Serializable]
    public class AboutViewModel : ViewModel
    {
    }
}