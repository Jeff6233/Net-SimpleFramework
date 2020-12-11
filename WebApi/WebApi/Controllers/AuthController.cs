using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using WebApi.Services;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    public class AuthController : ApiController
    {
        [Route("token")]
        [HttpPost]
        public IHttpActionResult Token()
        {
            if (ModelState.IsValid)//判断是否合法
            {
                //if (!(user == "abc" && password == "123456"))//判断账号密码是否正确
                //{
                //    return BadRequest();
                //}

                var tokenString=JsonWebTokenHelper.CreateToken("ff", "Admin");

                return Json(new
                {
                    access_token = tokenString,
                    token_type = "Bearer",
                    createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });

            }
            return Json(new
            {
                err = "Not found",
                message = "Bad Request"
            });
        }

        [Route("try")]
        [HttpGet]
        [OAuth2]
        public IHttpActionResult Try()
        {
            return Ok("Success");
        }
    }
}