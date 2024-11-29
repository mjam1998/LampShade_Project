

using _01_LampShadeQuery.Contracts.Article;
using System.Collections.Generic;

namespace _01_LampShadeQuery.Contracts.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string Description { get;  set; }
        public int ShowOrder { get;  set; }
        public string Slug { get;  set; }
        public string MetaDescription { get;  set; }
        public string KeyWord { get;  set; }
        public List<string> Keywords { get; set; }
        public string CanonicalAdress { get;  set; }
        public long ArticleCount { get; set; }
       public List<ArticleQueryModel> Articles { get; set; }
    }
}
