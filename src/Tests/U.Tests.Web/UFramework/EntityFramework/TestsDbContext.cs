using System.Data.Entity;
using U.EntityFramework;

namespace U.Tests.Web.UFramework
{
    public class TestsDbContext : UDbContext
    {
        public virtual IDbSet<Article> Articles { get; set; }

        public TestsDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
    }
}