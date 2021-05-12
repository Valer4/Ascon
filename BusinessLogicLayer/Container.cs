using Microsoft.Practices.Unity;
using System;

namespace BusinessLogicLayer
{
    public class Container
    {
        public IUnityContainer _main = new UnityContainer();
        public IUnityContainer Main => _main;

        public void RegisterType<TInterface, TClass>(params object[] constructorParams) =>
            Main.RegisterType(
                typeof(TInterface),
                typeof(TClass),
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(constructorParams));

        public T Resolve<T>() => Main.Resolve<T>();
        public object Resolve(Type type) => Main.Resolve(type);
    }
}
