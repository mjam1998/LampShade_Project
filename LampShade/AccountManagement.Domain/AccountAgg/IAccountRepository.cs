

using _0_Freamwork.Domain;
using AccountManagement.Application.Contract.AccountAppContract;
using System.Collections.Generic;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository:IRepository<long, Account>
    {
        EditAccount GetDetails(long id);
        Account GetBy(string username);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
