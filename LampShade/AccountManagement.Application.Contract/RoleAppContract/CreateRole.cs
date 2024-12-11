
using _0_Freamwork.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.RoleAppContract
{
    public class CreateRole
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Name { get; set; }
    }

}
