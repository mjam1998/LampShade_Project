
using System.Collections.Generic;

using _0_Freamwork.Domain;
using ShopManagement.Application.Contracts.SlideAppContract;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository:IRepository<long,Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
