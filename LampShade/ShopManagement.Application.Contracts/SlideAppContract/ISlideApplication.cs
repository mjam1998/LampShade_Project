using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Freamwork.Application;

namespace ShopManagement.Application.Contracts.SlideAppContract
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide command);
        OperationResult Edit(EditSlide command);
        OperationResult Remove(long id);
        OperationResult ReStore(long id);
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
