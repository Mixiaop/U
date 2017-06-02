using System.ComponentModel.DataAnnotations.Schema;
using U.Domain.Entities.Auditing;
using U.Domain.Repositories;
using U.Application.Services;
using U.EntityFramework;

namespace U.Tests.Web.UFramework
{
    [Table("Articles")]
    public class Article : CreationAuditedEntity
    {
        public string Title { get; set; }
    }

    public interface IArticleRepository : IRepository<Article>
    {

    }

    public class ArticleRepository : TestsRepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(IDbContextProvider<TestsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }




    public interface IArticleService : IApplicationService
    {
        void Insert(Article article);
    }

    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;
        public ArticleService(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
            var a = 1;
        }
        public void Insert(Article article)
        {
            _articleRepository.Insert(article);
        }
    }



}