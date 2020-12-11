using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;
using WebApi.Domain;
using WebApi.Domain.Repository;

namespace WebApi.Service
{
    public interface IUnityInjector
    {
        TSource GetService<TSource>();
        WebAppContext Db { get; }
    }
    public class UnityInjector: IUnityInjector
    {
        private IUnityContainer unity;

        public UnityInjector(IUnityContainer unity)
        {
            this.unity = unity;
            this.Register();
        }

        private void Register()
        {
            this.unity.RegisterInstance<IUnityInjector>(this);
            this.unity.RegisterSingleton<WebAppContext, WebAppContext>();
            this.unity.RegisterType<IT_InputVGMRepository, T_InputVGMRepository>();
            this.unity.RegisterType<IT_InputVGMService, T_InputVGMService>();
        }

        public TSource GetService<TSource>()
        {
            return this.unity.Resolve<TSource>();
        }

        public WebAppContext Db 
        {
            get=> this.unity.Resolve<WebAppContext>();
        }
    }
}
