

using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using BlogManagement.Application.contract.ArticleCategoryAppContract;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryRepository(BlogContext blogContext):base(blogContext) 
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
           return _blogContext.ArticleCategories.Select(x=>new ArticleCategoryViewModel
           {
               Id = x.Id,
               Name = x.Name
           }).ToList();
        }

        public EditArticleCategory GetDetails(long id)
        {
           return _blogContext.ArticleCategories.Select(x=>new EditArticleCategory
           {
               Id = id,
               Name = x.Name,
               Description = x.Description,
               CanonicalAdress = x.CanonicalAdress,
               KeyWords = x.KeyWords,
               MetaDescription = x.MetaDescription,
               ShowOrder = x.ShowOrder, 
               Slug = x.Slug,
               PictureAlt = x.PictureAlt,
               PictureTitle = x.PictureTitle

           }).FirstOrDefault(x=>x.Id == id);
        }

        public string GetSlugBy(long id)
        {
            return _blogContext.ArticleCategories.Select(x => new {x.Id,x.Slug}).FirstOrDefault(x=>x.Id == id).Slug;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _blogContext.ArticleCategories.Include(x=>x.Articles).Select(x=>new ArticleCategoryViewModel
            {
                Id=x.Id,
                 Name = x.Name,
                 Description= x.Description,
                 Picture = x.Picture,
                 Slug= x.Slug,
                 ShowOrder= x.ShowOrder,
                 CreationDate=x.CreationDate.ToFarsi(),
                 ArticleCount=x.Articles.Count
            });

             if(!string.IsNullOrWhiteSpace(searchModel.Name))
                query=query.Where(x=>x.Name.Contains(searchModel.Name));    

             return query.OrderByDescending(x=>x.ShowOrder).ToList();  
        }
    }
}
