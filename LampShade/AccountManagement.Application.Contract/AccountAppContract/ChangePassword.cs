

using _0_Freamwork.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.AccountAppContract
{
    public class ChangePassword
    {
        public long Id { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string RePassword { get; set; }
    }
}
