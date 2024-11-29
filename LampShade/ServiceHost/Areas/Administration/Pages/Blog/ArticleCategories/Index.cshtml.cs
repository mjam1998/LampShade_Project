﻿using System.Collections.Generic;

using BlogManagement.Application.contract.ArticleCategoryAppContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {
        public List<ArticleCategoryViewModel> ArticleCategories;
        public ArticleCategorySearchModel SearchModel;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
           var result= _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editArticleCategories = _articleCategoryApplication.GetDetails(id);
            return Partial("Edit", editArticleCategories);
        }

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            //اتربیوت هایی که نوشتیم مثل fileExtention اگر ولید باشد دستور را اجرا میکند سرور ساید
            
            
                var result = _articleCategoryApplication.Edit(command);
                return new JsonResult(result);
            
            
        }
    }
}
