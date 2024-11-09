

using System.Collections.Generic;
using _0_Freamwork.Application;

namespace DiscountManagement.Application.Contract.CustomerDiscountAppContract
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscount command);
        OperationResult Edit(EditCustomerDiscount command);
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
