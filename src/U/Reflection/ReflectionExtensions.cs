using System.Reflection;

namespace U.Relfection
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets methods defined attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this MethodBase methodInfo) where T : class
        {
            return methodInfo.GetCustomAttribute(typeof(T)) as T;
        }
    }
}
