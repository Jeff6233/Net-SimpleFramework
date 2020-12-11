using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace WebApi
{
    public class UnityConfig : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IUnityContainer unity;
        private UnityScope scope;
        public UnityConfig(IUnityContainer unity)
        {
            this.unity = unity;
            this.scope = new UnityScope(unity);
        }

        public IDependencyScope BeginScope() => this.scope;

        public void Dispose()
        {
            this.unity.Dispose();
            this.scope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            if (this.unity.IsRegistered(serviceType))
                return this.unity.Resolve(serviceType) ?? throw new ApplicationException($"Container中未注册接口/服务:{serviceType.FullName}");
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType) => this.unity.ResolveAll(serviceType);
    }

    public class UnityScope : IDependencyScope
    {
        private IUnityContainer unity;
        public UnityScope(IUnityContainer unity)
        {
            this.unity = unity;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public object GetService(Type serviceType)
        {
            return this.unity.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.unity.ResolveAll(serviceType);
        }
    }
}