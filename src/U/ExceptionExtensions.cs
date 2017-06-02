using System;
using System.Threading;
using System.Security;
using System.Runtime.InteropServices;
namespace U.Exceptions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 抛出的导常是否为致命的
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool IsFatal(this Exception ex)
        {
            return ex is UException ||
                ex is StackOverflowException ||
                ex is OutOfMemoryException ||
                ex is AccessViolationException ||
                ex is AppDomainUnloadedException ||
                ex is ThreadAbortException ||
                ex is SecurityException ||
                ex is SEHException;
        }
    }
}
