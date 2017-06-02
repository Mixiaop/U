using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using U.Logging;

namespace U.Settings
{
    public abstract class USettings<T> : USettings where T : class
    {
        PropertyInfo[] _properties;
        IEnumerable<PropertyInfo> Properties
        {
            get
            {
                return _properties ?? (_properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty));
            }
        }
        protected ILogger Logger { get; set; }

        public USettings()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 更新配置，如果成功更新则返回true
        /// </summary>
        /// <param name="newSettings"></param>
        /// <returns></returns>
        public virtual bool UpdateSettings(T newSettings)
        {
            if (newSettings == null)
            {
                return false;
            }

            bool changed = false;
            try
            {
                var toCopy = Properties;
                foreach (var prop in toCopy)
                {
                    if (!prop.CanWrite) continue;

                    var current = prop.GetValue(this);
                    var newSetting = prop.GetValue(newSettings);

                    // observables are meant to be updated at the member level and need specific love
                    if (prop.PropertyType.IsGenericType &&
                        prop.PropertyType.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
                    {
                        // handle collections!
                        // TODO: Decide on how collections (and subcollections) are handled
                        // need to broadcast these changes or publish them via json APIs
                    }
                    else if (prop.CanWrite)
                    {
                        try
                        {
                            if (current == newSetting) continue; // nothing changed, NEXT
                            prop.SetValue(this, newSetting);
                            OnPropertyChanged(prop.Name);
                            changed = true;
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Error setting propery: " + prop.Name, e);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Error updating settings for " + typeof(T).Name, e);
            }

            if (changed)
                TriggerChanged();
            return changed;
        }
    }

    /// <summary>
    /// 基类,代表一个配置
    /// </summary>
    public abstract class USettings :  INotifyPropertyChanged
    {
        public USettings() { }

        ///// <summary>
        ///// 是否启用本配置
        ///// </summary>
        //public abstract bool Enabled { get; set; }

        public event EventHandler Changed;

        protected void TriggerChanged()
        {
            var handler = Changed;
            if (handler != null)
                handler.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
