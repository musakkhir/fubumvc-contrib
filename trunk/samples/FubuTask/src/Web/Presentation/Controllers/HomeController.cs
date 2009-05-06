namespace FubuTask.Presentation.Controllers
{
    public class HomeController
    {
        public IndexModel Index(IndexModel model)
        {
            return model;
        }
    }

    public class IndexModel : ViewModel
    {
    }
}