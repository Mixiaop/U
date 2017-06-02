using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using Autofac;
using U.Domain.Uow;
using U.EntityFramework;

namespace U.Tests.Web.UFramework
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IDbContextProvider<TestsDbContext> provider = UPrimeEngine.Instance.Resolve<IDbContextProvider<TestsDbContext>>();
            //var a = 1;
            if (!IsPostBack)
            {
                //IArticleRepository articleRepository = UPrimeEngine.Instance.Resolve<IArticleRepository>();
                
                //articleRepository.Insert(article);

                // var articleService = UPrimeEngine.Instance.Resolve<IArticleService>();
                //articleService.Insert(article);

                Article article = new Article();
                article.Title = "title1";
                //IConventionalDbProvider dbProvider = new ConventionalDbProvider(new ConventionalDbContext());
                ConventionalRepository articleRepository = new ConventionalRepository(new ConventionalDbProvider(new ConventionalDbContext()));

                articleRepository.Insert(article);
            }

        }
    }

    //public class AppService : U.Application.Services.IApplicationService {
    //    public virtual string HelloWorld() { 
    //        return "hello world";
    //    }
    //}
}