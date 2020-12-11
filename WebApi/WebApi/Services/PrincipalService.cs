using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Web;

namespace WebApi.Services
{
    internal class UserRole
    {
        public string Admin { get; set; } = "管理员";
        public string Employee { get; set; } = "职员";
        public string Common { get; set; } = "普通用户";
    }

    public class PrincipalService : System.Security.Principal.IPrincipal
    {
        private Identity user;
        public IIdentity Identity => user;
        public string Role { get; set; }

        public bool IsInRole()
        {
            if (!string.IsNullOrEmpty(this.Role))
            {
                Type userRole = typeof(UserRole);
                var role = userRole.GetProperty(this.Role);
                if (role == null)
                    return false;
                return true;
            }
            return false;
        }

        [Obsolete]
        public bool IsInRole(string role)
        {
            return true;
        }

        public void CreatePrincipal(Identity user)
        {
            if (user != null && user.IsAuthenticated)
            {
                this.user = user;
            }
            else
            {
                throw new SecurityException("没有有效用户，无法创建主体");
            }
        }
    }

    public class Identity : IIdentity
    {
        public Identity(string name, string authenticationType, bool isAuthenticated)
        {
            this.Name = name;
            this.AuthenticationType = authenticationType;
            this.IsAuthenticated = isAuthenticated;
        }

        public string Name { get; }

        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; }
    }
}