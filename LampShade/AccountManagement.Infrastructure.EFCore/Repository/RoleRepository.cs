

using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using AccountManagement.Application.Contract.RoleAppContract;
using AccountManagement.Domain.RoleAgg;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _context;

        public RoleRepository(AccountContext context):base(context) 
        {
            _context = context;
        }

        public EditRole GetDetails(long id)
        {
           return _context.Roles.Select(x=>new EditRole
           {
               Id=x.Id,
               Name=x.Name,
           }).FirstOrDefault(x=>x.Id == id);
        }

        public List<RoleViewModel> List()
        {
            return _context.Roles.Select(x=>new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate=x.CreationDate.ToFarsi()
            }).ToList();
        }
    }
}
