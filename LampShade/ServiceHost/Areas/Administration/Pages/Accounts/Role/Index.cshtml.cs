using System.Collections.Generic;
using AccountManagement.Application.Contract.AccountAppContract;
using AccountManagement.Application.Contract.RoleAppContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<RoleViewModel> Roles;
       
        

        private readonly IRoleApplication _roleApplication;

        public IndexModel(IRoleApplication roleApplication)
        {
           
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            Roles = _roleApplication.List();
          
        }

     

       
      


    }
}
