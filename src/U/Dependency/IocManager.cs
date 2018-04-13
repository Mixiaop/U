using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Linq;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;

namespace U.Dependency
{
    /// <summary>
    /// Ioc管理器，所有的依赖注册类型及任务都由它来执行
    /// </summary>
    public class IocManager : IIocManager
    {
        #region Properties & Ctor
        /// <summary>
        /// autofac 容器
        /// </summary>
        public IContainer IocContainer { get; private set; }

        /// <summary>
        /// 依赖注册器列表
        /// </summary>
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        public IocManager(IContainer container)
        {
            IocContainer = container;
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();
        }
        #endregion

        #region IIocRegistrar
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar) {
            _conventionalRegistrars.Add(registrar);
        }

        public void RegisterAssemblyByConvention(Assembly assembly) {
            var context = new ConventionalRegistrationContext(assembly, this);

            foreach (var registerer in _conventionalRegistrars)
            {
                registerer.RegisterAssembly(context);
            }
        }

        public void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "") where T : class
        {
            Register(typeof(T), typeof(T), lifeStyle, namedAlias);
        }

        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "")
        {
            Register(type, type, lifeStyle, namedAlias);
        }

        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "")
        {
            Register(typeof(TType), typeof(TImpl), lifeStyle, namedAlias);
        }

        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "")
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    if (namedAlias.IsNotNullOrEmpty())
                    {
                        builder.RegisterType(impl).As(type).Named(namedAlias, type).SingleInstance();
                    }
                    else {
                        builder.RegisterType(impl).As(type).SingleInstance();
                    }
                    break;
                case DependencyLifeStyle.Transient:
                    if (namedAlias.IsNotNullOrEmpty())
                    {
                        builder.RegisterType(impl).As(type).Named(namedAlias, type).InstancePerDependency();
                    }
                    else {
                        builder.RegisterType(impl).As(type).InstancePerDependency();
                    }
                    break;
            }
            builder.Update(IocContainer);
        }

        #endregion

        #region IIocResolver
        public T Resolve<T>(string namedAlias = "") where T : class
        {
            var obj = Resolve(typeof(T), namedAlias);
            return obj as T;
        }

        public object Resolve(Type type, string namedAlias = "")
        {
            var scope = Scope();
            if (namedAlias.IsNotNullOrEmpty())
            {
                return scope.ResolveNamed(namedAlias, type);
            }
            else {
                return scope.Resolve(type);
            }
        }

        public T[] ResolveAll<T>() {
            var scope = Scope();
            var list = scope.Resolve<IEnumerable<T>>().ToArray();

            return list;

        }

        public T ResolveUnregistered<T>() where T : class
        {
            return ResolveUnregistered(typeof(T)) as T;
        }

        public object ResolveUnregistered(Type type)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null) throw new UException("Unkown dependency");
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (Exception)
                {

                }
            }
            throw new UException("No contructor was found that had all the dependencies satisfied.");
        }

        /// <summary>
        /// Release pre-resolved object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete("释放预解析的对象 Castle.Windsor 有使用到，Autofac 自动释放")]
        public void Release(object obj)
        {
        }

        public bool IsRegistered<T>(string namedAlias = "")
        {
            return IsRegistered(typeof(T), namedAlias);
        }

        /// <summary>
        /// 检查指定的类型是否注册
        /// </summary>
        /// <param name="type">检查的类型</param>
        /// <returns></returns>
        public bool IsRegistered(Type type, string namedAlias = "")
        {
            var scope = Scope();
            if (namedAlias.IsNotNullOrEmpty())
            {
                return scope.IsRegisteredWithName(namedAlias, type);
            }
            else
            {
                return scope.IsRegistered(type);
            }
        }
        #endregion

        public void Dispose()
        {
            IocContainer.Dispose();
        }

        #region Utilities 
        /// <summary>
        /// 之前使用过因为Nop，后来弃用了直接使用 Container 解析
        /// </summary>
        /// <returns></returns>
        private ILifetimeScope Scope()
        {
            //return this.IocContainer;
            try
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                    return IocContainer.BeginLifetimeScope();
            }
            catch (Exception exc)
            {
                //如果 RequestLifetimeScope 已经被释放了，我们可以获取一个异常
                //例如, 请求后或在 "Application_EndRequest" 事件后处理的
                //但是通常不会发生
                //为什么使用LifretimeScope来解析，而不使用IocContainer.Resolve https://nblumhardt.com/2011/01/an-autofac-lifetime-primer/
                //跟内存有关

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                //return IocContainer.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
                return IocContainer.BeginLifetimeScope();
            }
        }
        #endregion
    }
}
