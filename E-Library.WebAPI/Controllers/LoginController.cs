using E_Library.Business.Contracts;
using E_Library.Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_Library.WebAPI.Controllers
{
    //[Authorize]
    [RoutePrefix("Login")]
    public class LoginController : ApiController
    {
        ILogin LoginService = new LoginService(WebApiApplication.AppKeys);
        [HttpPost]
        [Route("Authenticate_User")]
        public HttpResponseMessage Authenticate_User(Entities.Users user)
        {
            try
            {
                var result = LoginService.Authenticate_User(user);
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
                    return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
                }
            }
        }
    }
}
