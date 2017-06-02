using System.Collections.Generic;

namespace U.Application.Services.Dto
{
    /// <summary>
    /// 业务处理后返回的操作结果
    /// </summary>
    public class StateOutput : IOutputDto
    {
        private IList<string> _errors;

        public StateOutput()
        {
            _errors = new List<string>();
        }

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success
        {
            get { return _errors.Count == 0; }
        }

        /// <summary>
        /// 错误消息记录
        /// </summary>
        public IList<string> Errors
        {
            get
            {
                return _errors;
            }
        }

        /// <summary>
        /// 添加一个错误消息
        /// </summary>
        /// <param name="error">错误消息</param>
        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }

    /// <summary>
    /// 业务处理后返回的操作结果
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    public class StateOutput<T> : StateOutput
    {
        /// <summary>
        /// 操作成功，返回的单条结果对象
        /// </summary>
        public T Items { get; set; }
    }
}
