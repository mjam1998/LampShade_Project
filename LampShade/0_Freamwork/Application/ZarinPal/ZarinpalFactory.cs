using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.Json;
using System;




namespace _0_Framework.Application.ZarinPal
{
    public class ZarinPalFactory : IZarinPalFactory
    {
        private readonly IConfiguration _configuration;

        public string Prefix { get; set; }
        private string MerchantId { get;}
        //string merchant = "cfa83c81-89b0-4993-9445-2c3fcd323455";

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            //میرود از appsetting.json  یا appsetting.development.json  میخواتد
            Prefix = _configuration.GetSection("payment")["method"];//اینجا میفهمد در چه محیطی هستیم sandbox  توسعه یا www عملیاتی
            MerchantId= _configuration.GetSection("payment")["merchant"];
        }

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
             long orderId)
        {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];

            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/v4/payment/request.json");
            var request = new RestRequest($"https://{Prefix}.zarinpal.com/pg/v4/payment/request.json", Method.Post);//درخواست پرداخت درست میکند
            //ریکوست json  است
            request.AddHeader("Content-Type", "application/json");
            var body = new PaymentRequest
            {
                Mobile = mobile,
                Callback_url = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",//پرداخت انجام به کدام صفحه برگردد
                Description = description,
                Email = email,
                Amount = finalAmount,
                Merchant_id = MerchantId
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);//اینجا درخواست پرداخت ا میفرستد
            JObject jodata = JObject.Parse(response.Content);
            var result = new PaymentResponse();
            result.Authority= jodata["data"]["authority"].ToString();
            result.Status = Convert.ToInt32(jodata["data"]["code"]);
            //result.Status= jodata.Value<int>("status");


            return result;
            //  var jsonSerializer = new JsonSerializer();
            // return jsonSerializer.Deserialize<PaymentResponse>(jo);
        }

        public VerificationResponse CreateVerificationRequest(string authority, string amount)
        {
            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/v4/payment/verify.json");
            var request = new RestRequest($"https://{Prefix}.zarinpal.com/pg/v4/payment/verify.json", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            request.AddJsonBody(new VerificationRequest
            {
                Amount = finalAmount,
                Merchant_id = MerchantId,
                Authority = authority
            });
            var response = client.Execute(request);
            JObject jodata = JObject.Parse(response.Content);
            var result = new VerificationResponse();
            result.Status= Convert.ToInt32(jodata["data"]["code"]);
            result.RefID = Convert.ToInt64(jodata["data"]["ref_id"]);
            // result.RefID = jodata.Value<long>("ref_id");
            return result;
            //var jsonSerializer = new JsonSerializer();
            //return jsonSerializer.Deserialize<VerificationResponse>(response);
        }
    }
}