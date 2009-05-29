using System.IO;
using System.Web;
using FubuMVC.Tests;
using FubuTask.Presentation.Services;
using NUnit.Framework;

namespace FubuTask.Tests.Presentation.Services
{
    [TestFixture]
    public class when_setting_response_status_properties
    {
        private HttpResponse _response;
        private ResponseStatusService _service;

        [SetUp]
        public void SetUp()
        {
            _response = new HttpResponse(new StringWriter());
            _service = new ResponseStatusService(){Response = _response};
        }

        [Test]
        public void should_reflect_code_on_http_response()
        {
            _service.Status = 404;
            _response.StatusCode.ShouldEqual(404);
        }

        [Test]
        public void should_reflect_description_on_http_response()
        {
            _service.Description = "Not Found";
            _response.StatusDescription.ShouldEqual("Not Found");
        }

    }
}