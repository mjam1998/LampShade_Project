using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.ProductAppContract;


namespace ShopManagement.Application.Contracts.ProductPictureAppContract
{
    public class CreateProductPicture
    {
        [Range(1,10000,ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
