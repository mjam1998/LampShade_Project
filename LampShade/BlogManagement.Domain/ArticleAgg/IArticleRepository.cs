

using _0_Freamwork.Domain;
using BlogManagement.Application.contract.ArticleAppContract;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long,Article>
    {
        EditArticle GetDetails(long id);
        Article GetWithCategory(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
