

using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using AccountManagement.Application.Contract.AccountAppContract;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context):base(context) 
        {
            _context = context;
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _context.Accounts.Select(x =>new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName
            }).ToList();   
        }

        public Account GetBy(string username)
        {
           return _context.Accounts.FirstOrDefault(x=>x.UserName == username);  
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x=>new EditAccount
            {
                 Id = x.Id,
                 FullName = x.FullName,
                 Mobile = x.Mobile,
                 Password = x.Password,
                 RoleId = x.RoleId,
                 UserName = x.UserName,
                

            }).FirstOrDefault(x=>x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Include(x=>x.Role).Select(x => new AccountViewModel
            { 
                Id= x.Id,
                FullName= x.FullName,
                Mobile= x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                Role=x.Role.Name,
                RoleId= x.RoleId,
                UserName= x.UserName,
                CreationDate=x.CreationDate.ToFarsi()
            });
            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (searchModel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
