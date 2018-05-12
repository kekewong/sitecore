using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SingleSignOn.Core.Installer.Framework;
using SingleSignOn.Core.Mediators.Messages;

namespace SingleSignOn.Api
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public HttpResponseMessage Post(CreateUserMessage message)
        {
            var output = _mediator.Send(message);
            return Request.CreateResponse(output.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest, output);
        }
    }
}