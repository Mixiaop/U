using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using U.Settings;
using U.Startup;
using U.Tests.Web.EntityFramework;
using U.Runtime.Caching;
using U.Runtime.Caching.Memory;
using U.Runtime.Caching.Redis;

namespace U.Tests.Web.Caching
{
    public partial class RedisCachingTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnGet.Click += BtnGet_Click;
            btnUpdate.Click += BtnUpdate_Click;
            //var settings = UPrimeEngine.Instance.Resolve<TestsCacheSettings>();
            //Response.Write(JsonConvert.SerializeObject(settings));
            //var areaService = UPrimeEngine.Instance.Resolve<IAreaService>();
            //Response.Write(areaService.Get(1));

            //UPrimeEngine.Instance.Register<ICacheManager, TestsCacheManager>(U.Dependency.DependencyLifeStyle.Singleton, "ZeroB");


            //var zeroCacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>("zero");
            //var oneCacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>("one");


            //var areaService = UPrimeEngine.Instance.Resolve<IAreaService>();
            //Response.Write(areaService.Get());
            //Response.Write(areaService.Get());
            //var areaOneService = UPrimeEngine.Instance.Resolve<IAreaOneService>();
            //Response.Write(areaOneService.Get("你好啊啊啊啊", "你好啊啊啊啊"));

            //var zeroCacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>("zero");
            //zeroCacheManager.GetCache("AreaService").RemoveByPattern("Get[5rW36bmP");

            IAreaService areaService = UPrimeEngine.Instance.Resolve<IAreaService>();
            var a = 1;
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            var areaService = UPrimeEngine.Instance.Resolve<IAreaService>();
            var key = string.Format("AreaService_Get[{0}]", ("海鹏").EncodeUTF8Base64() + "|" + ("hello").EncodeUTF8Base64());
            areaService.Get("海鹏", "hello");
            Response.Write(key + "<br />");

            var areaOneService = UPrimeEngine.Instance.Resolve<IAreaOneService>();
            var key2 = string.Format("AreaOneService_Get[{0}]", ("肥施").EncodeUTF8Base64() + "|" + ("hello").EncodeUTF8Base64());

            areaOneService.Get("肥施", "hello");
            Response.Write(key2 + "<br />");

            areaService.Get();

            //Response.Write(areaService.Get());

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            var areaService = UPrimeEngine.Instance.Resolve<IAreaService>();
            areaService.Update("肥施");
        }
    }

    #region Zero
    [USettingsPathArribute("ZeroCacheSettings.json", "/Config/Tests/")]
    public class ZeroCacheSettings : USettings<ZeroCacheSettings>
    {
        public int Enabled { get; set; }

        public int RedisEnabled { get; set; }
        public string RedisConnectionString { get; set; }

        public int RedisDatabaseId { get; set; }
    }

    public class ZeroRedisCacheDatabaseProvider : IURedisCacheDatabaseProvider
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        private readonly ZeroCacheSettings _settings;
        public ZeroRedisCacheDatabaseProvider(ZeroCacheSettings settings)
        {
            _settings = settings;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_settings.RedisDatabaseId);
        }

        public ISubscriber GetSubscriber()
        {
            return _connectionMultiplexer.Value.GetSubscriber();
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_settings.RedisConnectionString);
        }
    }

    public class ZeroCacheManager : CacheManagerBase
    {
        private readonly ZeroCacheSettings _settings;
        public ZeroCacheManager(IUStartupConfiguration configuration, ZeroCacheSettings settings) : base(configuration)
        {
            _settings = settings;
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            if (_settings.Enabled == 1)
            {
                if (_settings.RedisEnabled == 1)
                {
                    return new URedisCache(name, UPrimeEngine.Instance.Resolve<ZeroRedisCacheDatabaseProvider>());
                }
                else
                {
                    return new UMemoryCache(name);
                }
            }
            else {
                return new NullCache("");
            }
        }
    }
    #endregion

    #region One
    [USettingsPathArribute("OneCacheSettings.json", "/Config/Tests/")]
    public class OneCacheSettings : USettings<OneCacheSettings>
    {
        public int RedisEnabled { get; set; }
        public string RedisConnectionString { get; set; }

        public int RedisDatabaseId { get; set; }
    }

    public class OneRedisCacheDatabaseProvider : IURedisCacheDatabaseProvider
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        private readonly OneCacheSettings _settings;
        public OneRedisCacheDatabaseProvider(OneCacheSettings settings)
        {
            _settings = settings;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_settings.RedisDatabaseId);
        }

        public ISubscriber GetSubscriber()
        {
            return _connectionMultiplexer.Value.GetSubscriber();
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_settings.RedisConnectionString);
        }
    }

    public class OneCacheManager : CacheManagerBase
    {
        private readonly OneCacheSettings _settings;
        public OneCacheManager(IUStartupConfiguration configuration, OneCacheSettings settings) : base(configuration)
        {
            _settings = settings;
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            if (_settings.RedisEnabled == 1)
            {
                return new URedisCache(name, UPrimeEngine.Instance.Resolve<OneRedisCacheDatabaseProvider>());
            }
            else
            {
                return new UMemoryCache(name);
            }
        }
    }
    #endregion

    #region Services 

    public interface IAreaService : U.Application.Services.IApplicationService
    {
        string Get();

        string Get(string id, string id2);

        void Update(string id);

    }

    public class AreaService : IAreaService
    {
        IAreaRepository _areaRepository;
        ICacheManager _cacheManager;
        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
            _cacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>();
            var b = 1;
        }

        [Caching("zero", CachingBehavior.Get)]
        public virtual string Get()
        {

            return "Get - no parameters";
        }

        [Caching("zero", CachingBehavior.Get)]
        public virtual string Get(string id, string id2)
        {
            return id + id2;
        }


        //[Caching("zero", CachingBehavior.Remove,
        //                 "Get")]

        [Caching("zero", CachingBehavior.Remove,
                         "Get(KeyUserId)",
                         "AreaOneService_Get(KeyUserId)")]

        public virtual void Update(string id)
        {
            KeyUserId = id.ToString();
        }

        public string KeyUserId { get; set; }
    }

    public interface IAreaOneService : U.Application.Services.IApplicationService
    {
        string Get(string id, string id2);

    }

    public class AreaOneService : IAreaOneService
    {
        IAreaRepository _areaRepository;
        public AreaOneService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        [Caching("one", CachingBehavior.Get)]
        public virtual string Get(string id, string id2)
        {
            return id + id2 + "one";
        }
    }
    #endregion
}

