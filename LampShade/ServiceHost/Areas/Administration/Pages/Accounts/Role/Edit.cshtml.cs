using _0_Freamwork.Infrastructure;
using AccountManagement.Application.Contract.RoleAppContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        public EditRole Command;
       // public List<SelectListItem> Permissions;
       
        private readonly IRoleApplication _roleApplication;
       // private readonly IEnumerable<IPermissionExposer> _permissionsExposer;

        public EditModel(IRoleApplication roleApplication)//,IEnumerable<IPermissionExposer> permissionExposers)
        {
            _roleApplication = roleApplication;
          //  _permissionsExposer = permissionExposers;
        }

        public void OnGet(long id)
        {
            Command=_roleApplication.GetDetails(id);
        }
       

        public IActionResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
