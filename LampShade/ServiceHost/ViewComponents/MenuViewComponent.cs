using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    { 
        private readonly IProductCategoryQuery _productCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCategories=_productCategoryQuery.GetProductCategories();
            return View("_Menu",productCategories);
        }
    }
}
