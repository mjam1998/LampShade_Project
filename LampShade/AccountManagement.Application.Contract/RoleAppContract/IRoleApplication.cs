

using _0_Freamwork.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contract.RoleAppContract
{
    public interface IRoleApplication
    {
        OperationResult Create( CreateRole command );
        OperationResult Edit( EditRole command );
        List<RoleViewModel> List();
        EditRole GetDetails(long id );
    }
}
