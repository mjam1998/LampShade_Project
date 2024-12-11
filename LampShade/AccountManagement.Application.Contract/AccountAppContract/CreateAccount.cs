

using _0_Freamwork.Application;
using AccountManagement.Application.Contract.RoleAppContract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.AccountAppContract
{
    public class CreateAccount
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string FullName { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get;  set; }
       
        public long RoleId { get;  set; }
        public IFormFile ProfilePhoto { get;  set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}