

using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.ProductAppContract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.InventoryAppContract
{
    public class CreateInventory
    {
        [Range(1,100000000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get;  set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public double UnitPrice { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
