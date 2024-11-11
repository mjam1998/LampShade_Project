

using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.ProductAppContract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contract.ColleagueDiscountAppContract
{
    public class DefineColleagueDiscount
    {
        [Range(1, 1000000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get;  set; }
        [Range(1,99,ErrorMessage =ValidationMessages.IsRequired)]
        public int DiscountRate { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
    
}
