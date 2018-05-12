using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SingleSignOn.Core.Installer.Framework;
using SingleSignOn.Core.Mediators.Messages;
using SingleSignOn.Core.Services;

namespace SingleSignOn.Api
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IAuthCacheService _authCacheService;

        public AuthController(IMediator mediator, IAuthCacheService authCacheService)
        {
            _mediator = mediator;
            _authCacheService = authCacheService;
        }

        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(SignInMessage message)
        {
            var output = _mediator.Send(message);
            return Request.CreateResponse(output.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest, output);
        }

        [HttpGet]
        [Route("CheckUserSession")]
        public HttpResponseMessage CheckUserSession(string username)
        {
            var user = _authCacheService.Check(username);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("CheckAvailability")]
        public HttpResponseMessage CheckAvailability()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Auth Service Is Up");
        }
    }
}
