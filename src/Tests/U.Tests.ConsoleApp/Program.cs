using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U.Tests.ConsoleApp.CastleDynamicProxy;
using U.Tests.ConsoleApp.AutofacUtils;

namespace U.Tests.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //new InterceptorTests().Run();

            //new AutofacInterceptorTests().Run();
            var list = new List<Title>();
            list.Add(new Title() { Name = "Title1" });
            list.Add(new Title() { Name = "Title2" });
            list.Add(new Title() { Name = "Title3" });
            list.Add(new Title() { Name = "Title4" });
            list.Add(new Title() { Name = "Title5" });

            var columnNames = string.Join(", ", list.Select(p => p.Name)).Trim();

            Console.WriteLine(columnNames);

        }
    }

    public class Title {
        public string Name { get; set; }
    }
}
