using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using U.Web.Utils;
using U.Logging;
using U.Startup.Configuration;

namespace U.Settings
{
    public class JSONSettingsManager : ISettingsManager
    {
        #region Fields & Ctor
        private ISettingsConfiguration _settingsConfig;
        private readonly object _loadLock = new object();
        private readonly ConcurrentDictionary<Type, object> _settingsCache = new ConcurrentDictionary<Type, object>();
        private string _defaultPath;
        public ILogger Logger { get; set; }

        public JSONSettingsManager(ISettingsConfiguration settingsConfig)
        {
            _settingsConfig = settingsConfig;
            Logger = NullLogger.Instance;

            _defaultPath = RequestHelper.MapPath(_settingsConfig.SettingsPath);

            if (_defaultPath.StartsWith("~\\"))
                _defaultPath = _defaultPath.Replace("~\\", AppDomain.CurrentDomain.BaseDirectory);
        }
        #endregion

        public T GetSettings<T>() where T : USettings<T>, new()
        {
            object cached;
            if (_settingsCache.TryGetValue(typeof(T), out cached))
                return (T)cached;

            lock (_loadLock)
            {
                if (_settingsCache.TryGetValue(typeof(T), out cached))
                    return (T)cached;

                var settings = GetFromFile<T>();
                if (settings == null)
                    return null;
                AddUpdateWatcher(settings);
                _settingsCache.TryAdd(typeof(T), settings);
                return settings;
            }
        }

        public T SaveSettings<T>(T settings) where T : USettings<T>, new()
        {
            lock (_loadLock)
            {
                File.WriteAllText(GetFullFileName<T>(), JsonConvert.SerializeObject(settings));
                _settingsCache.TryAdd(typeof(T), settings);
                return settings;
            }
           
        }

        /// <summary>
        /// 重置所有设置
        /// </summary>
        public void ResetAllSettings()
        {
            _settingsCache.Clear();
        }

        #region Utilities
        private void AddUpdateWatcher<T>(T settings) where T : USettings<T>, new()
        {
            var path = GetSettingsPath<T>();

            var watcher = new FileSystemWatcher(path.Path, path.Name)
            {
                NotifyFilter = NotifyFilters.LastWrite
            };
            watcher.Changed += (s, args) =>
            {
                var newSettings = GetFromFile<T>();
                settings.UpdateSettings(newSettings);
            };

            watcher.EnableRaisingEvents = true;
        }

        private string GetFullFileName<T>()
        {
            var path = GetSettingsPath<T>();

            return System.IO.Path.Combine(path.Path, path.Name);
        }

        private T GetFromFile<T>() where T : new()
        {
            var path = GetFullFileName<T>();
            try
            {
                if (!File.Exists(path))
                {
                    return new T();
                }

                using (var sr = File.OpenText(path))
                {
                    var serializer = new JsonSerializer
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    return (T)serializer.Deserialize(sr, typeof(T));
                }
            }
            catch (Exception e)
            {
                // A race on reloads can happen - ignore as this is during shutdown
                if (!e.Message.Contains("The process cannot access the file"))
                    Logger.Error("Error loading settings from " + path, e);
                return default(T);
            }
        }

        private USettingsPath GetSettingsPath<T>()
        {
            USettingsPath settingPath = new USettingsPath();
            if (typeof(T).IsDefined(typeof(USettingsPathArribute), true))
            {
                var attr = typeof(T).GetCustomAttribute(typeof(USettingsPathArribute), true) as USettingsPathArribute;
                settingPath.Path = attr.GetAbsolutePath();
                settingPath.Name = attr.Name;
            }

            if (string.IsNullOrEmpty(settingPath.Path))
                settingPath.Path = _defaultPath;

            if (string.IsNullOrEmpty(settingPath.Name))
            {
                settingPath.Name = typeof(T).Name + ".json";
            }

            return settingPath;
        }
        #endregion

    }
}
