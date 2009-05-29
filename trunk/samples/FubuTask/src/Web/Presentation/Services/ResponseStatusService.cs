using System.Web;

namespace FubuTask.Presentation.Services
{
    public class ResponseStatusService : IResponseStatusService
    {
        private HttpResponse _response;

        public HttpResponse Response
        {
            get { return _response ?? HttpContext.Current.Response; }
            set { _response = value; }
        }

        public int Status
        {
            get { return Response.StatusCode; }
            set { Response.StatusCode = value; }
        }

        public string Description
        {
            get { return Response.StatusDescription; }
            set { Response.StatusDescription = value; }
        }
    }
}