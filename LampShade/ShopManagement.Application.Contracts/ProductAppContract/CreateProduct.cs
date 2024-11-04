﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.ProductCategoryAppContract;

namespace ShopManagement.Application.Contracts.ProductAppContract
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get;  set; }
        [Range(0,500000000,ErrorMessage = ValidationMessages.IsRequired)]
        public double UnitPrice { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        [Range(1,1000000,ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get;  set; }
        public List<ProductCategoryViewModel> Categories { get; set; }
       
    }
}
