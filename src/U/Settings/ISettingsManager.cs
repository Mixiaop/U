
namespace U.Settings
{
    public interface ISettingsManager
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetSettings<T>() where T : USettings<T>, new();


        /// <summary>
        /// 保存配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T SaveSettings<T>(T settings) where T : USettings<T>, new();

        /// <summary>
        /// 重置所有设置
        /// </summary>
        void ResetAllSettings();
    }
}
