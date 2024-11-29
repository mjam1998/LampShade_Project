

using _0_Freamwork.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.contract.ArticleAppContract
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);
        EditArticle GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
