

namespace InventoryManagement.Application.Contract.InventoryAppContract
{
    public class InventorySearchModel
    {
        public string Product { get; set; }
        public long ProductId { get; set; }
        public bool InStock { get; set; }
    }
}
