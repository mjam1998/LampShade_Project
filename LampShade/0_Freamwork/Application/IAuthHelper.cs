

namespace _0_Freamwork.Application
{
    public interface IAuthHelper
    {
        void Signin(AuthViewModel account);
        bool IsAuthenticated();
        void Signout();
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        long CurrentAccountId();
    }
}
