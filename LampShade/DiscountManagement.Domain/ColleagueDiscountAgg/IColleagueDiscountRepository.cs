

using _0_Freamwork.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscountAppContract;
using System.Collections.Generic;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository:IRepository<long,ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
