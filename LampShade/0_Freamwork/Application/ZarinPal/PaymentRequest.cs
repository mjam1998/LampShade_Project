﻿namespace _0_Framework.Application.ZarinPal
{
    public class PaymentRequest
    {
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Callback_url { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Merchant_id { get; set; }
    }
}
