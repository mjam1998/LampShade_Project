

using _0_Freamwork.Domain;
using BlogManagement.Domain.ArticleAgg;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory:EntityBase
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public string KeyWords { get; private set; }
        public string CanonicalAdress { get; private set; }
        public List<Article> Articles { get; private set; }

        public ArticleCategory(string name, string picture, string description, 
            int showOrder, string slug, string metaDescription, string keyWords, string canonicalAdress, string pictureAlt, string pictureTitle)
        {
            Name = name;
            Picture = picture;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            MetaDescription = metaDescription;
            KeyWords = keyWords;
            CanonicalAdress = canonicalAdress;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }
        public void Edit(string name, string picture, string description,
            int showOrder, string slug, string metaDescription, string keyWords, string canonicalAdress, string pictureAlt, string pictureTitle)
        {
            Name = name;
            if(!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            MetaDescription = metaDescription;
            KeyWords = keyWords;
            CanonicalAdress = canonicalAdress;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }
    }
}
