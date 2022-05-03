using System;
using System.Collections.Generic;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace BusinessLogicLayer.DiContainer
{
    public class DiContainer : IDiContainer
    {
        private IUnityContainer Implementation { get; } = new UnityContainer().AddExtension(new Diagnostic());

        public Dictionary<Type, Func<object>> ResolveDelegateDictionary = new Dictionary<Type, Func<object>>();
        public Dictionary<Type, Type> ResolveParamTypeDictionary = new Dictionary<Type, Type>();

        public void RegisterType<TInterface, TClass>(DiLifetimeType diLifetimeType, params object[] constructorParams)
        {
            Implementation.RegisterType(
                typeof(TInterface),
                typeof(TClass),
                GetLifetimeType(diLifetimeType),
                new InjectionConstructor(constructorParams));
        }
        public void RegisterType<TClass>(DiLifetimeType diLifetimeType, params object[] constructorParams)
        {
            Implementation.RegisterType(
                typeof(TClass),
                GetLifetimeType(diLifetimeType),
                new InjectionConstructor(constructorParams));
        }

        public void RegisterType<TInterface, TClass>(Type paramType, Func<object> resolveDelegate)
        {
            ResolveParamTypeDictionary.Add(typeof(TInterface), paramType);
            ResolveParamTypeDictionary.Add(typeof(TClass), paramType);

            ResolveDelegateDictionary.Add(typeof(TInterface), resolveDelegate);
            ResolveDelegateDictionary.Add(typeof(TClass), resolveDelegate);

            RegisterType<TInterface, TClass>(DiLifetimeType.Transient, resolveDelegate());
        }
        public void RegisterType<TClass>(Type paramType, Func<object> resolveDelegate)
        {
            ResolveParamTypeDictionary.Add(typeof(TClass), paramType);

            ResolveDelegateDictionary.Add(typeof(TClass), resolveDelegate);

            RegisterType<TClass>(DiLifetimeType.Transient, resolveDelegate());
        }

        public T Resolve<T>() => (T)Resolve(typeof(T));
        public object Resolve(Type type)
        {
            if (ResolveDelegateDictionary.TryGetValue(type, out Func<object> resolveDelegate))
            {
                ResolveParamTypeDictionary.TryGetValue(type, out Type paramType);
                return Implementation.Resolve(type, new ParameterOverride(paramType, resolveDelegate()));
            }
            return Implementation.Resolve(type);
        }

        private ITypeLifetimeManager GetLifetimeType(DiLifetimeType? diLifetimeType = null)
        {
            switch(diLifetimeType)
            {
                default: return TypeLifetime.Transient;
            }
        }
    }
}
