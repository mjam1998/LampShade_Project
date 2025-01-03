using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _0_Framework.Application.ZarinPal;
using _0_Freamwork.Application.Sms;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;




namespace _0_Framework.Application.Sms
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string number, string token1,string token2)
        {
            // توکن ها بر اساس الگوی نوشته شده در سایت خاوند نگار میباشد و اسم کاربر میشود توکن1 وکد پیگیری میشود توکن2 
            var updateToken=token1.Replace(" ", "_");
            var apiKey= _configuration.GetSection("SmsSecrets")["ApiKey"];
            var template= _configuration.GetSection("SmsSecrets")["Template1"];
            var url = "api.kavenegar.com";
            var client = new RestClient($"https://{url}/v1/{apiKey}/verify/lookup.json?receptor={number}&token={updateToken}&token2={token2}&template={template}");
            var request = new RestRequest($"https://{url}/v1/{apiKey}/verify/lookup.json?receptor={number}&token={updateToken}&token2={token2}&template={template}", Method.Post);
            //request.AddHeader("Content-Type", "application/json");
            //var body = new SmsRequest
            //{
            //     receptor=number,
            //     token=token1,
            //     token2 = token2,
            //     template=template
            //};
            //request.AddJsonBody(body);
            var response = client.Execute(request);
            JObject jodata = JObject.Parse(response.Content);



        }
    }
}
   