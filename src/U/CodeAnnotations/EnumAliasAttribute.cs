using System;
using System.Reflection;

namespace U.CodeAnnotations
{
    /// <summary>
    /// 属性：枚举的别名定义
    /// </summary>
    public class EnumAliasAttribute : Attribute
    {
        private string _alias;
        public EnumAliasAttribute(string alias)
        {
            _alias = alias;
        }

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
    }
}
