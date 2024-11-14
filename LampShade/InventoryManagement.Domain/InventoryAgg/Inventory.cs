

using _0_Freamwork.Domain;
using System.Collections.Generic;
using System.Linq;


namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long ProductId { get; private set; }
        public long Count { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId,double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }
        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }
        public long CalculateCurrentCount()
        {
            var plus=Operations.Where(x=>x.Operation).Sum(x=>x.Count);
            var mines = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - mines;
        }
        public void Increase(long count,long operatorId,string descriptoin)
        {
            var currentCount=CalculateCurrentCount()+ count;
            var operation= new InventoryOperation(true,count,operatorId,currentCount,descriptoin,0,Id);
            Operations.Add(operation);
            InStock=currentCount > 0;
            
        }
        public void Reduce(long count, long operatorId, string descriptoin,long orderId)
        {
            var currentCount=CalculateCurrentCount() - count;
            var operation=new InventoryOperation(false,count,operatorId,currentCount, descriptoin,orderId,Id);
            Operations.Add(operation );
            InStock=currentCount > 0;
        }
    }
}
