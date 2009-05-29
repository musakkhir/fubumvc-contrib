using FubuMVC.Tests;
using FubuTask.Presentation.Controllers;
using FubuTask.Presentation.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuTask.Tests.Presentation.Controllers
{
    [TestFixture]
    public class when_navigating_to_the_not_found_url
    {
        private IResponseStatusService _service;
        private PageNotFoundController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = MockRepository.GenerateStub<IResponseStatusService>();
            _controller = new PageNotFoundController(_service);
        }

        [Test]
        public void should_set_the_response_status_code()
        {
            _controller.Index(null);
            _service.Status.ShouldEqual(404);
        }
    }
}