namespace _0_Framework.Application.ZarinPal
{
    public interface IZarinPalFactory
    {
        string Prefix { get; set; }// پراپرتی که به ما میگوید در چه محیطی زرین پال را تست کنیم اگر sandbox  باشد توسعه و اگر www  باشد عملیاتی
        //درخواست پرداخت
        PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
            long orderId);
        //تایید پرداخت 
        VerificationResponse CreateVerificationRequest(string authority, string price);
    }
}