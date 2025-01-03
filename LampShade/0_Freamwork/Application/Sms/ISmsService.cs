namespace _0_Framework.Application.Sms
{
    public interface ISmsService
    {
        void Send(string number, string token1, string token2);
    }
}