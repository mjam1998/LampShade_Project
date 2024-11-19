

using Microsoft.AspNetCore.Http;

namespace _0_Freamwork.Application
{
    public interface IFileUploader
    {
        string Upload (IFormFile file,string path);
    }
}
