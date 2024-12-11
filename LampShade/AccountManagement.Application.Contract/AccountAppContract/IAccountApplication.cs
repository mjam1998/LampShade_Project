

using _0_Freamwork.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contract.AccountAppContract
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult Login( Login command);
        EditAccount GetDetails(long id);
        
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        void Logout();
    }
}
