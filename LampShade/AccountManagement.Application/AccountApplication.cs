

using _0_Framework.Application;
using _0_Freamwork.Application;
using AccountManagement.Application.Contract.AccountAppContract;
using AccountManagement.Domain.AccountAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;

        public AccountApplication(IAccountRepository accountRepository,IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation= new OperationResult();
            var account=_accountRepository.Get(command.Id);
            if(account==null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);
            
            var password=_passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.UserName == command.UserName|| x.Mobile==command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var password = _passwordHasher.Hash(command.Password);
            var path = "ProfilePhotos";
            var pictureName = _fileUploader.Upload(command.ProfilePhoto, path);
            var account=new Account(command.FullName,command.UserName,command.Mobile,password,command.RoleId,pictureName);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succedded();

              
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountRepository.Exists(x =>
                (x.UserName == command.UserName || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var path = $"profilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.FullName, command.UserName, command.Mobile, command.RoleId, picturePath);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditAccount GetDetails(long id)
        {
           return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.UserName);
            if(account == null)
                return operation.Failed(ApplicationMessages.WrongUserName);

            (bool Verified, bool NeedsUpgrade) result= _passwordHasher.Check(account.Password, command.Password);
            if(!result.Verified)
               
                return operation.Failed(ApplicationMessages.WrongPassword);
            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.FullName, account.UserName);
            _authHelper.Signin(authViewModel);

            return operation.Succedded();
        }

        public void Logout()
        {
            _authHelper.Signout();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }
    }
}
