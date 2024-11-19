

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace _0_Freamwork.Application
{
    public class FileExtentionLimitionAttribute : ValidationAttribute, IClientModelValidator
    {
        //میخواهیم نوع پذیرش فایل ها را با ارایه معلوم کنیم مثل jpg , png
        private readonly string[] _validExtention;
        public FileExtentionLimitionAttribute(string[] validExtention)
        {
            _validExtention = validExtention;
        }
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) { return true; }
            //پسوند فایل را میگیرد
            var fileExtention=Path.GetExtension(file.FileName);
            return _validExtention.Contains(fileExtention);
        }
        //ایین متد پایین و اینترفیس برای کلاینت ولیدیشن است که باید با جی کویری نوشاه شود
        public void AddValidation(ClientModelValidationContext context)
        {
           // context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fileExtentionLimit", ErrorMessage);
        }
    }
}
