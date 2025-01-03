

using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        //مبلغ  بدون تخفیف
        public double TotalAmount { get; set; }
        //مبلغ تخفیف
        public double DiscountAmount { get; set; }
        //مبلغ نهایی قابل پرداخت با اعمال تخفیف
        public double PayAmount { get; set; }
        public int PaymentMethod { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void AddItem(CartItem cartItem)
        {
            Items.Add(cartItem);
            TotalAmount=TotalAmount+cartItem.TotalItemPrice;
            DiscountAmount=DiscountAmount+cartItem.DiscountAmount;
            PayAmount=PayAmount+cartItem.ItemPayAmount;
        }

        public void SetPaymentMethod(int methodId)
        {
            PaymentMethod = methodId;
        }
    }
}
