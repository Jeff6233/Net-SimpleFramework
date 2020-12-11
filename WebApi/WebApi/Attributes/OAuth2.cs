using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApi.Services;


namespace WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class OAuth2Attribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentException("actionContext");
            }
            HttpControllerContext context = actionContext.ControllerContext;
            if (context.Request.Headers.Authorization == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("未授权访问") };
                return;
            }

            var (isAuthorized, jwtIdentity) = JsonWebTokenHelper.ValidateToken(context.Request.Headers.Authorization.Parameter);

            if (!isAuthorized)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("未授权访问") };
                return;
            }
            Identity identity = new Identity(jwtIdentity.Name, context.Request.Headers.Authorization.Scheme, jwtIdentity.IsAuthenticated);
            PrincipalService p = new PrincipalService();
            p.CreatePrincipal(identity);
            p.Role = ((ClaimsIdentity)jwtIdentity).Claims.ToList().Find(i => i.Type == ClaimTypes.Role).Value;
            if (!p.IsInRole())
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("用户未授予身份资格") };
                return;
            }
            context.RequestContext.Principal = p;
            base.OnAuthorization(actionContext);
        }

    }
}