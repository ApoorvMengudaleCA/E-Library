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
    [RoutePrefix("Roles")]
    public class RolesController : ApiController
    {
        IRoles RolesService = new RolesService(WebApiApplication.AppKeys);
        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage RolesList()
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

        [HttpGet]
        [Route("GetDataById")]
        public HttpResponseMessage GetData(int ID)
        {
            try
            {
                var result = RolesService.GetData(ID)
;
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

        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage Save(Entities.Roles roles)
        {
            try
            {
                var result = RolesService.Save(roles);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        [HttpGet]
        [Route("Delete")]
        public HttpResponseMessage Delete(int Id, int UpdatedBy)
        {
            try
            {
                bool result = RolesService.Delete(Id, UpdatedBy);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                else
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

    }
}
