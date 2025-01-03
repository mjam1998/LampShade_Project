

using _0_Freamwork.Domain;
using System.Collections.Generic;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order:EntityBase
    {
        public long AccountId { get; private set; }
        public double TotalAmount { get; private set; }
        public int PaymentMethod { get;  private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        //شماره پیگیری خرید
        public string IssueTrackingNo { get; private set; }
        //شماره پیگیری درگاه بانک 
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId, double totalAmount, double discountAmount,
            double payAmount,int paymentMethod)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
           
       
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
            PaymentMethod = paymentMethod;
            Items= new List<OrderItem>();
        }

        public void PaymentSucceeded( long refId)
        {
            IsPaid = true;
          
                RefId= refId;
        }
        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo= number;
        }
        public void Cancel()
        {
            IsCanceled = true;
        }
        public void AddItems(OrderItem item)
        {
            Items.Add(item);
        }
    }
}
