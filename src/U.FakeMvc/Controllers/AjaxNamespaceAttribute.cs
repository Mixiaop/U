using System;

namespace U.FakeMvc.Controllers
{
     [AttributeUsage(AttributeTargets.Class)]
    public class AjaxNamespaceAttribute : Attribute
    {
         private string _name;
         public AjaxNamespaceAttribute(string name)
         {
             _name = name;
         }
    }
}
