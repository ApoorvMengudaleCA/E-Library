using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_Library.Business.Contracts;
using E_Library.Business.Services;

namespace E_Library.WebAPI.Controllers
{
    //[Authorize]
    [RoutePrefix("Login")]
    public class LoginController : ApiController
    {
        ILogin LoginService = new LoginService(WebApiApplication.AppKeys);
        [HttpGet]
        [Route("Authenticate_User")]
        public HttpResponseMessage Authenticate_User(string UserName, string UserPassword)
        {
            try
            {
                var result = LoginService.Authenticate_User(UserName, UserPassword);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, false);

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Record Not Found"))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
                }
            }
        }
    }
}
