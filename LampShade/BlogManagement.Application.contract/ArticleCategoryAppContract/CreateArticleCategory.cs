

using _0_Freamwork.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.contract.ArticleCategoryAppContract
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Name { get;  set; }
        public IFormFile Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public int ShowOrder { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string KeyWords { get;  set; }
        public string CanonicalAdress { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get;  set; }
    }
}
