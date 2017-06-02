
namespace U.Logging
{
    /// <summary>
    /// 接口定义日志的等级
    /// </summary>
    public interface IHasLogLevel
    {
        LogLevel LogLevel { get; set; }
    }
}
