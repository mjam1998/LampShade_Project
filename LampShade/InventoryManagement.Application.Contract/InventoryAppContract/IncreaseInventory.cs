﻿

namespace InventoryManagement.Application.Contract.InventoryAppContract
{
    public class IncreaseInventory
    {
        public long InventoryId { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }

    }
}
