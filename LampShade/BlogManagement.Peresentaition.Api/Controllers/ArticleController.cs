using _01_LampShadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogManagement.Peresentaition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleQuery _articleQuery;

        public ArticleController(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        [HttpGet]
        public List<ArticleQueryModel> GetLatestArticle()
        {
            return _articleQuery.LatestArticles();
        }
    }
}
