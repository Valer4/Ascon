using System;

namespace BusinessLogicLayer.DiContainer
{
	public interface IDiContainer
	{
		void RegisterType<TInterface, TClass>(DiLifetimeType diLifetimeType, params object[] constructorParams);
		void RegisterType<TClass>(DiLifetimeType diLifetimeType, params object[] constructorParams);

		void RegisterType<TInterface, TClass>(Type paramType, Func<object> resolveDelegate);
		void RegisterType<TClass>(Type paramType, Func<object> resolveDelegate);

		T Resolve<T>();
		object Resolve(Type type);
	}
}
