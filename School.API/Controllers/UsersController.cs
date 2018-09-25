using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace School.API.Controllers
{
    using System;
    using System.IO;
    using System.Web.Http;
   
    using Common.Models;

    using Newtonsoft.Json.Linq;
    using School.API.Helpers;

    [RoutePrefix("api/Users")]
    public class UserController : ApiController
    {
        public IHttpActionResult PostUser(UserRequest userRequest)
        {
            if (userRequest.ImageArray != null && userRequest.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(userRequest.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Users";
                var fullPath = $"{folder}/{file}";

            }

            var answer = UsersHelper.CreateUserASP(userRequest);
            return Ok(answer);
        }

        [HttpPost]
        [Route("GetUser")]
        public IHttpActionResult GetUser(JObject form)
        {
            try
            {
                var email = string.Empty;
                dynamic jsonObject = form;

                try
                {
                    email = jsonObject.Email.Value;
                }
                catch
                {
                    return BadRequest("Incorrect call.");
                }

                var user = UsersHelper.GetUserASP(email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
