using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Attributes;
using WebApi.Service;
using WebApi.Services;

namespace WebApi.Controllers
{
    //[EnableCors("*", "*", "*")]
    public class TestController : ApiController
    {
        private IT_InputVGMService t_InputVGMService;
        private IUnityService unity;

        public TestController(IT_InputVGMService inputVGMService, IUnityService unity)
        {
            t_InputVGMService = inputVGMService;
            this.unity = unity;
        }

        [Route("List")]
        [HttpGet]
        public IHttpActionResult List(int? pageIndex,int? pageSize)
        {
            var (list,total)=unity.GetService<IT_InputVGMService>().List(pageIndex.GetValueOrDefault(1), pageSize.GetValueOrDefault(10));
            return Json(new {List=list,Total=total });
        }

        [Route("upload")]
        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                var file = fileCollection[i];
                var fileName=file.FileName;
                var s = file.InputStream;
                ExcelHelper excel = new ExcelHelper(fileName, s);
                var list=excel.ReadExcel(1);
                this.t_InputVGMService.Add(list[0]);
            }
            return Ok();
        }

        [Route("t")]
        [HttpGet]
        public void Login(string u,string p)
        {
            var user = new Identity(u,this.ActionContext.Request.Headers.Authorization.Scheme, true);
            var principal = new PrincipalService();
            principal.CreatePrincipal(user);
            this.User = principal;
            HttpContext.Current.User = principal;
        }

        [Route("f")]
        [HttpGet]
        [OAuth2]
        public void FFF()
        {
            var s = this.User;
            var a = HttpContext.Current.User;
        }
    }
}
