using _0_Freamwork.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public  string Upload(IFormFile file,string path)
        {
            if(file == null) { return ""; }
           var  directoryPath = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{path}";

            if(!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            //اسم عکس ها را با تاریخ یکتا میکنیمs
            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            var filePath = $"{directoryPath}//{fileName}";
            using var output= File.Create(filePath);
            file.CopyTo(output);

            return $"{path}//{fileName}";
        }
    }
}
