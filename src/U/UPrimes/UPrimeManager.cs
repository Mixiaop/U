using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using U.Logging;

namespace U.UPrimes
{
    /// <summary>
    /// 模块管理器，用于管理所有的UPrime
    /// </summary>
    internal class UPrimeManager : IUPrimeManager
    {
        #region Properties  & Ctor
        public ILogger Logger { get; set; }


        private readonly UPrimeCollection _uprimes;
        private readonly IUPrimeFinder _uprimeFinder;
        private readonly UPrimeEngine _engine;

        public UPrimeManager(IUPrimeFinder uprimeFinder)
        {
            _uprimes = new UPrimeCollection();
            _uprimeFinder = uprimeFinder;
            Logger = NullLogger.Instance;
            _engine = UPrimeEngine.Instance;
        }
        #endregion

        public virtual void InitializeUPrimes()
        {
            LoadAll();

            var sortedUPrimes = _uprimes.GetSortedUPrimeListByDependency();

            sortedUPrimes.ForEach(module => module.Instance.PreInitialize());
            sortedUPrimes.ForEach(module => module.Instance.Initialize());
            sortedUPrimes.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownUPrimes()
        {
            var sortedUPrimes = _uprimes.GetSortedUPrimeListByDependency();
            sortedUPrimes.Reverse();
            sortedUPrimes.ForEach(sm => sm.Instance.Shutdown());
        }

        #region Utitiles
        private void LoadAll()
        {
            Logger.Debug("Loading UPrimes...");

            var uprimeTypes = AddMissingDependedUPrimes(_uprimeFinder.FindAll());
            Logger.Debug("Found " + uprimeTypes.Count + " UPrimes in total.");

            //Register to Ioc container
            foreach (var uprimeType in uprimeTypes)
            {
                if (!UPrime.IsUPrime(uprimeType))
                {
                    throw new UException("This type is not an UPrime：" + uprimeType.AssemblyQualifiedName);
                }
                if (!_engine.IocManager.IsRegistered(uprimeType))
                {
                    _engine.IocManager.Register(uprimeType);
                }
            }

            //add to UPrime collection
            foreach (var uprimeType in uprimeTypes)
            {
                var uprimeObject = (UPrime)_engine.IocManager.Resolve(uprimeType);
                uprimeObject.Engine = _engine;
                _uprimes.Add(new UPrimeInfo(uprimeObject));

                Logger.Debug(string.Format("Loaded UPrime: {0}", uprimeType.AssemblyQualifiedName));
            }

            EnsureLeadershipUPrimeToBeFirst();
            SetDependencies();

            Logger.Debug(string.Format("{0} UPrime loaded.", _uprimes.Count));
        }

        private void EnsureLeadershipUPrimeToBeFirst()
        {
            var leadershipUPrimeIndex = _uprimes.FindIndex(m => m.Type == typeof(ULeadershipUPrime));
            if (leadershipUPrimeIndex > 0)
            {
                var leadershipUPrime = _uprimes[leadershipUPrimeIndex];
                _uprimes.RemoveAt(leadershipUPrimeIndex);
                _uprimes.Insert(0, leadershipUPrime);
            }
        }

        private void SetDependencies()
        {
            foreach (var uprimeInfo in _uprimes)
            {
                //根据程序集的依赖性设置模块的依赖
                foreach (var referencedAssemblyName in uprimeInfo.Assembly.GetReferencedAssemblies())
                {
                    var referencedAssembly = Assembly.Load(referencedAssemblyName);
                    var dependedUPrimeList = _uprimes.Where(m => m.Assembly == referencedAssembly).ToList();
                    if (dependedUPrimeList.Count > 0)
                    {
                        uprimeInfo.Dependencies.AddRange(dependedUPrimeList);
                    }
                }

                //从定义的DependsOnAttribute特性来设置模块的依赖
                foreach (var dependedUPrimeType in UPrime.FindDependedUPrimeTypes(uprimeInfo.Type))
                {
                    var dependedUPrimeInfo = _uprimes.FirstOrDefault(m => m.Type == dependedUPrimeType);
                    if (dependedUPrimeInfo == null)
                    {
                        throw new UException("Could not find a depended UPrime " + dependedUPrimeType.AssemblyQualifiedName + " for " + uprimeInfo.Type.AssemblyQualifiedName);
                    }

                    if ((uprimeInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedUPrimeType) == null))
                    {
                        uprimeInfo.Dependencies.Add(dependedUPrimeInfo);
                    }
                }
            }
        }


        /// <summary>
        /// 添加遗漏掉的依赖模块
        /// </summary>
        /// <param name="allModules"></param>
        /// <returns></returns>
        private static ICollection<Type> AddMissingDependedUPrimes(ICollection<Type> allUPrimes)
        {
            var initialPrimes = allUPrimes.ToList();
            foreach (var prime in initialPrimes)
            {
                FillDependedUPrimes(prime, allUPrimes);
            }

            return allUPrimes;
        }

        /// <summary>
        /// 添加依赖模块
        /// </summary>
        /// <param name="module"></param>
        /// <param name="allModules"></param>
        private static void FillDependedUPrimes(Type uprime, ICollection<Type> allUPrimes)
        {
            foreach (var dependedUPrime in UPrime.FindDependedUPrimeTypes(uprime))
            {
                if (!allUPrimes.Contains(dependedUPrime))
                {
                    allUPrimes.Add(dependedUPrime);
                    FillDependedUPrimes(dependedUPrime, allUPrimes);
                }
            }
        }
        #endregion
    }
}
