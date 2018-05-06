using PassPilot_v2._0.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace PassPilot_v2._0
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        public static IUnityContainer Container
        {
            get { return _container; }
        }

        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<ISiteData, JsonSiteData>(
                new ContainerControlledLifetimeManager());
        }

    }
}
