

using _0_Freamwork.Application;
using _01_LampShadeQuery.Contracts.Article;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampShadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
          return _blogContext.ArticleCategories.Include(x=>x.Articles).Select(x=>new ArticleCategoryQueryModel 
          {
               Name = x.Name,
               Picture = x.Picture,
               PictureAlt = x.PictureAlt,
               PictureTitle = x.PictureTitle,
               Slug = x.Slug,
               ArticleCount=x.Articles.Count()
          }).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
           var articleCategory= _blogContext.ArticleCategories.Include(x=>x.Articles).Select(x=>new ArticleCategoryQueryModel
           {
               Name=x.Name,
               Description=x.Description,
               Slug=x.Slug,
               Picture=x.Picture,
               PictureAlt=x.PictureAlt,
               PictureTitle=x.PictureTitle,
               KeyWord=x.KeyWords,
               MetaDescription=x.MetaDescription,
               CanonicalAdress=x.CanonicalAdress,
               ArticleCount=x.Articles.Count(),
              Articles=MapArticles(x.Articles)

           }).FirstOrDefault(x=>x.Slug==slug);
            if(!string.IsNullOrWhiteSpace(articleCategory.KeyWord))
            articleCategory.Keywords=articleCategory.KeyWord.Split(",").ToList();
            return articleCategory;
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x=>new ArticleQueryModel
            {
                Slug=x.Slug,
                Title=x.Title,
                ShortDescription=x.ShortDescription,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate=x.PublishDate.ToFarsi(),
            }).ToList();
        }
    }
}
