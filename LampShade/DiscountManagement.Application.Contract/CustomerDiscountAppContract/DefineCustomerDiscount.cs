



using System.Collections.Generic;
using _0_Freamwork.Application;
using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contracts.ProductAppContract;

namespace DiscountManagement.Application.Contract.CustomerDiscountAppContract
{
    public class DefineCustomerDiscount
    {
        [Range(1, 1000000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get;  set; }
        [Range(1, 99, ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get;  set; }
        public string Reason { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
