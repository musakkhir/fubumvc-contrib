using System;
using FubuTask.Presentation.Services;

namespace FubuTask.Presentation.Controllers
{
    public class PageNotFoundController
    {
        private readonly IResponseStatusService _responseStatusService;

        public PageNotFoundController(IResponseStatusService responseStatusService)
        {
            _responseStatusService = responseStatusService;
        }

        public object Index(object input)
        {
            _responseStatusService.Status = 404;
            return new object();
        }
    }
}