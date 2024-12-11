

using _0_Freamwork.Domain;
using AccountManagement.Application.Contract.RoleAppContract;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository:IRepository<long,Role>
    {
        List<RoleViewModel> List();
        EditRole GetDetails(long id);
    }
}
