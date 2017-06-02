using System;
using System.Reflection;
using U.UI;
using U.FakeMvc.Controllers;

namespace U.FakeMvc.UI
{
    public abstract class PageBase : System.Web.UI.Page
    {
        public void RegisterScripts(string key, string scripts)
        {
            this.ClientScript.RegisterStartupScript(GetType(), key, scripts, true);
        }
    }

    //[Obsolete("2017-01-18更新，使用Controllers.ControlerBase<TModel>替换")]
    public abstract class PageBase<TCtrl, TModel> : PageBase where TCtrl : ControllerBase
    {
        private TCtrl _controller = Activator.CreateInstance<TCtrl>();
        private TModel _model;

        public PageBase()
        {

            //从控制器获取HttpGet Model
            foreach (var method in typeof(TCtrl).GetMethods())
            {
                if (method.IsDefined(typeof(HttpGetAttribute)) && method.ReturnType == typeof(TModel))
                {

                    var methodParams = method.GetParameters();
                    if (methodParams.Length > 0)
                    {
                        object[] paramList = new object[methodParams.Length];
                        var index = 0;
                        foreach (var param in methodParams)
                        {

                            if (param.ParameterType == typeof(string))
                            {
                                paramList[index] = Controller.HttpContext.Request.QueryString[param.Name];
                                if (paramList[index] == null)
                                {
                                    if (param.HasDefaultValue)
                                        paramList[index] = param.DefaultValue;

                                    paramList[index] = "";
                                }

                            }
                            else if (param.ParameterType == typeof(int))
                            {
                                var value = Controller.HttpContext.Request.QueryString[param.Name];
                                if (value.IsNullOrEmpty())
                                {
                                    if (param.HasDefaultValue)
                                        paramList[index] = param.DefaultValue;
                                    else
                                        paramList[index] = 0;
                                }
                                else
                                {
                                    paramList[index] = value.ToInt();
                                }
                            }
                            else
                            {
                                var value = Controller.HttpContext.Request.QueryString[param.Name];
                                if (value.IsNullOrEmpty())
                                {
                                    paramList[index] = null;
                                }
                                else
                                {
                                    paramList[index] = value;
                                }

                            }

                            index++;
                        }
                        _model = (TModel)method.Invoke(_controller, paramList);
                    }
                    else
                    {
                        _model = (TModel)method.Invoke(_controller, null);
                    }
                }
            }

            if (_model == null)
            {
                throw new UserFriendlyException("控制器未找到对应模型的Class.Method");
            }
        }

        public TCtrl Controller { get { return _controller; } }

        public TModel Model { get { return _model; } }
    }
}
