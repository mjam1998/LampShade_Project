
namespace _0_Freamwork.Application
{
    public static class Roles
    {
        public const string Admin = "1";
        public const string ContentUploader = "2";
        public const string SystemUser = "4";
        public const string Colleague = "6";

        public static string GetBy(long id)
        {
            switch (id)
            {
                case 1:
                    return "مدیر سیستم";
                case 2:
                    return "محتوا گذار";
                default: return "";
            }
        }
    }
}
