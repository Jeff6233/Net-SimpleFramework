using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using WebApi.Service;

namespace WebApi
{
    public interface IUnityService
    {
        TSource GetService<TSource>();
    }
    public class UnityService: IUnityService
    {
        private IUnityContainer unity;
        public UnityService()
        {
            this.unity = new UnityContainer();
            this.Resolver = new UnityConfig(this.unity);
            this.Register();
        }
        public UnityConfig Resolver { get; }

        public void Register()
        {
            this.unity.RegisterInstance<IUnityService>(this);
            new UnityInjector(this.unity);
        }

        public TSource GetService<TSource>()
        {
            return this.unity.Resolve<TSource>();
        }
    }
}