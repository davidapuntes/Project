using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace RssReader
{
    public class DependencyInjector
        
    {
        //Descargar nuget package unity
        private static readonly UnityContainer unityContainer = new UnityContainer();

        //RssHelper and FakeRssHelper (Types) both implement the interface IRssHelper
        //Aunque estos métodos los estamos creando genéricos, por los que valdrían para diferentes tipo e interfaces
        public static void Register<I, T>() where T : I
        {
            unityContainer.RegisterType<I, T>(new ContainerControlledLifetimeManager());
        }

        public static T Retrieve<T>()
        {
            //For retrieving the particular instances within the container (injected)
            return unityContainer.Resolve<T>();
        }

        public static void Inject<I>(I instance)
        {
            //Registering the different instances
            unityContainer.RegisterInstance<I>(instance, new ContainerControlledLifetimeManager());
        }
    }
}
