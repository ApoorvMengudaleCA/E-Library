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
    [RoutePrefix("Users")]
    public class UsersController : ApiController
    {
        IUsers UsersService = new UsersService(WebApiApplication.AppKeys);

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage UsersList()
        {
            try
            {
                var result = UsersService.GetAll();
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
                var result = UsersService.GetData(ID)
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

        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage Save(Entities.Users users)
        {
            try
            {
                var result = UsersService.Save(users);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Record Not Found") || ex.Message.Contains("Record already exists"))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
                }
            }
        }

        [HttpGet]
        [Route("Delete")]
        public HttpResponseMessage Delete(int ID, int UserId)
        {
            try
            {
                bool result = UsersService.Delete(ID, UserId);
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
