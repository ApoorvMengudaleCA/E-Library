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
    [Authorize]
    [RoutePrefix("Roles")]
    public class RolesController : ApiController
    {
        IRoles RolesService = new RolesService(WebApiApplication.AppKeys);
        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage BookList()
        {
            try
            {
                var result = RolesService.GetAll();
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

    }
}
