using System;
using System.Net;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace U.WebApi.Client
{
    /// <summary>
    /// Used to make requests to U based Web APIs.
    /// </summary>
    public interface IUWebApiClient
    {
        /// <summary>
        /// Base URL for all request. 
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Timeout value for all requests (used if not supplied in the request method).
        /// Default: 90 seconds.
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Used to set cookies for requests.
        /// </summary>
        Collection<Cookie> Cookies { get; }

        /// <summary>
        /// Request headers.
        /// </summary>
        ICollection<NameValue> RequestHeaders { get; }

        /// <summary>
        /// Response headers.
        /// </summary>
        ICollection<NameValue> ResponseHeaders { get; }

        /// <summary>
        /// Makes post request that does not get or return value.
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="timeout">Timeout as milliseconds</param>
        Task PostAsync(string url, int? timeout = null);

        /// <summary>
        /// Makes post request that gets input but does not return value.
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="input">Input</param>
        /// <param name="timeout">Timeout as milliseconds</param>
        Task PostAsync(string url, object input, int? timeout = null);

        /// <summary>
        /// Makes post request that does not get input but returns value.
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="timeout">Timeout as milliseconds</param>
        Task<TResult> PostAsync<TResult>(string url, int? timeout = null) where TResult : class;

        /// <summary>
        /// Makes post request that gets input and returns value.
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="input">Input</param>
        /// <param name="timeout">Timeout as milliseconds</param>
        Task<TResult> PostAsync<TResult>(string url, object input, int? timeout = null) where TResult : class;

        /// <summary>
        /// Makes get request that gets input and returns value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url"></param>
        /// <param name="input"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(string url, int? timeout = null) where TResult : class;
    }
}
