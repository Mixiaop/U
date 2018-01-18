using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace U.Runtime.Caching
{
    /// <summary>
    /// Caching属性拦截器
    /// 缓存键 = ClassName_MethodName(EncodeUTF8Base64(value1)|EncodeUTF8Base64(value2)|EncodeUTF8Base64(value3)...)
    /// </summary>
    public class CachingInterceptor : IInterceptor
    {
        public CachingInterceptor() {

        }

        public void Intercept(IInvocation invocation)
        {
            if (CachingHelper.HasCachingAttribute(invocation.Method))
            {
                var method = invocation.Method;
                if (method.IsDefined(typeof(CachingAttribute), false))
                {
                    CachingAttribute attr = (CachingAttribute)method.GetCustomAttributes(typeof(CachingAttribute), false)[0];
                    ICacheManager cacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>();
                    if (attr.CacheManagerProviderServiceName.IsNotNullOrEmpty()) {
                        cacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>(attr.CacheManagerProviderServiceName);
                    }

                    #region Cache key
                    string className = method.DeclaringType.Name;
                    string methodName = method.Name;
                    
                    if (invocation.Arguments != null) {
                        string values = "";
                        foreach (var arg in invocation.Arguments) {
                            values += arg.ToString().EncodeUTF8Base64() + "|";
                        }

                        if (values.IsNotNullOrEmpty()) {
                            methodName += string.Format("({0})", values.TrimEnd("|"));
                        }
                    }
                    #endregion

                    switch (attr.Behavior) {
                        case CachingBehavior.Get:
                            #region Get
                            var returnValue = cacheManager.GetCache(className).Get(methodName, () =>
                            {
                                invocation.Proceed();
                                return invocation.ReturnValue;
                            });

                            invocation.ReturnValue = returnValue;
                            #endregion
                            break;
                        case CachingBehavior.Set:
                            #region Set
                            invocation.Proceed();
                            cacheManager.GetCache(className).Set(methodName, invocation.ReturnValue);
                            #endregion
                            break;
                        case CachingBehavior.Remove:
                            #region Remove
                            invocation.Proceed();
                            if (attr.MethodNames != null) {
                                foreach (var removeKey in attr.MethodNames) {
                                    var serviceName = className;
                                    var keyName = removeKey;
                                    if (removeKey.Contains("_"))
                                    {
                                        string[] keyItems = removeKey.Split('_');
                                        if (keyItems.Length >= 2)
                                        {
                                            serviceName = keyItems[0].Trim();
                                            keyName = keyItems[1].Trim();
                                        }
                                    }

                                    #region have parameter
                                    if (keyName.Contains("(") && keyName.Contains(")"))
                                    {
                                        var keyNameParams = keyName.Split('(')[1].Replace(")", "").Trim().Split('|');

                                        if (keyNameParams.Length > 0) {
                                            foreach (var param in keyNameParams) {
                                                string paramValue = "";
                                                foreach (PropertyInfo info in method.DeclaringType.GetProperties()) {
                                                    if (param.Trim() == info.Name) {
                                                        paramValue = info.GetValue(invocation.InvocationTarget, null).ToString();
                                                        break;
                                                    }
                                                }
                                                keyName = keyName.Replace(param, paramValue.EncodeUTF8Base64());
                                            }
                                        }
                                        cacheManager.GetCache(serviceName).RemoveByPattern(keyName.TrimEnd(')'));
                                    }
                                    else {
                                        cacheManager.GetCache(serviceName).Remove(keyName);
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                            break;
                    }
                    
                }
            }
            else {
                invocation.Proceed();
            }
        }

        #region Private Methods

        #endregion
    }

}