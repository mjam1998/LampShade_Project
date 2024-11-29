using _01_LampShadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatesArticlesViewComponent:ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public LatesArticlesViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _articleQuery.LatestArticles();
            return View("_LatesArticles",articles);
        }
    }
}
