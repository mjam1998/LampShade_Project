

using _0_Freamwork.Domain;
using BlogManagement.Application.contract.ArticleCategoryAppContract;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IRepository<long, ArticleCategory> 
    {
        EditArticleCategory GetDetails(long id);
        string GetSlugBy(long id);
        List<ArticleCategoryViewModel> Search (ArticleCategorySearchModel searchModel);
        List<ArticleCategoryViewModel> GetArticleCategories();
    }
}
