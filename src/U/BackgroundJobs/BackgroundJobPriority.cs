
namespace U.BackgroundJobs
{
    /// <summary>
    /// Priority of background job
    /// 后台作业优先级，高的当前先执行
    /// </summary>
    public enum BackgroundJobPriority : byte
    {
        /// <summary>
        /// Low
        /// </summary>
        Low = 5,
        /// <summary>
        /// Below normal
        /// </summary>
        BelowNormal = 10,
        /// <summary>
        /// Normal (default)
        /// </summary>
        Normal  = 15,
        /// <summary>
        /// Above normal
        /// </summary>
        AboveNormal = 20,
        /// <summary>
        /// High
        /// </summary>
        High = 25
    }
}
