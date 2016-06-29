using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Component = Castle.MicroKernel.Registration.Component;
namespace IOC_DI
{
    public class clsWindsorRegistrar
    {

        public static void RegisterSingleton(Type interfaceType, Type implementationType)
        {
            clsIOC.Container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.Singleton);
        }

        public static IWindsorContainer GetContainer()
        {
            return clsIOC.Container;
        }

        public static void Register(Type interfaceType, Type implementationType)
        {
            clsIOC.Container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.Transient);

        }
        public static void RegisterAsFactory(Type interfaceType, Type implementationType)
        {
            clsIOC.Container.Register(Component.For(interfaceType).AsFactory().ImplementedBy(implementationType).LifeStyle.Transient);
        }

        public static void RegisterAllFromAssemblies(string a)
        {
            clsIOC.Container.Register(Classes.FromAssemblyNamed(a).Pick().WithServiceFirstInterface().Configure(o => o.LifestylePerWebRequest()));
        }
     


    }
}
