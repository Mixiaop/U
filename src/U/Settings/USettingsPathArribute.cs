using System;
using U.Web.Utils;

namespace U.Settings
{
    /// <summary>
    /// Used to define diy settings class mapping settings file（such as EmailSettings【/Config/U/EamilSettings.json】）
    /// </summary>
    public class USettingsPathArribute : Attribute
    {
        public string Name;
        public string RelativePath;
        public USettingsPathArribute(string name, string relativePath)
        {
            Name = name;
            RelativePath = relativePath;
        }

        public string GetAbsolutePath()
        {
            return RequestHelper.MapPath(RelativePath);
        }
    }
}
