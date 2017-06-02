using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace U.WebApi.Models
{
    /// <summary>
    /// 响应消息
    /// </summary>
    public class UResponseMessage
    {
        #region Fields & Ctor
        private UResponseStatusCode _code;
        private string _message;
        private DateTime _timestamp = DateTime.Now;
        public UResponseMessage()
        {
            _code = UResponseStatusCode.Ok;
            _message = string.Empty;
        }

        public UResponseMessage(UResponseStatusCode statusCode)
        {
            _code = statusCode;
            _message = statusCode.ToAlias();
        }

        public UResponseMessage(UResponseStatusCode statusCode, string message)
        {
            _code = statusCode;
            _message = message;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 状态码
        /// </summary>
        public UResponseStatusCode Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 设置自定义消息
        /// </summary>
        /// <param name="code"></param>
        public void SetMessage(UResponseStatusCode code)
        {
            SetMessage(code, code.ToAlias());
        }

        /// <summary>
        /// 设置自定义消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void SetMessage(UResponseStatusCode code, string message)
        {
            _code = code;
            _message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess() {
            return Code == UResponseStatusCode.Ok;
        }
        #endregion
    }

    /// <summary>
    /// 响应消息泛型实现
    /// </summary>
    public class UResponseMessage<T> : UResponseMessage where T : class
    {
        #region Fields & Ctor
        private IList<T> _results;
        private int _totalCount;
        private int _returnedCount;

        public UResponseMessage() : base() { }

        public UResponseMessage(UResponseStatusCode code, string message)
            : base(code, message) { }

        public UResponseMessage(UResponseStatusCode code, string message, IList<T> results, int totalCount)
            : base(code, message)
        {
            _results = results;
            _totalCount = totalCount;
            if (results != null)
                _returnedCount = results.Count;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount
        {
            get
            {
                if (_totalCount == 0)
                    _totalCount = this.ReturnedCount;

                return _totalCount;
            }
            set { _totalCount = value; }
        }

        /// <summary>
        /// 返回的记录数
        /// </summary>
        public int ReturnedCount
        {
            get
            {
                if (_returnedCount == 0 && _results != null)
                    return _results.Count;
                else
                    return _returnedCount;
            }
        }

        /// <summary>
        /// 返回的结果集
        /// </summary>
        public T Results
        {
            get;set;
        }
        #endregion
    }
}
