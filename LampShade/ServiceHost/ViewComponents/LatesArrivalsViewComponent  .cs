using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatesArrivalsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatesArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var products=_productQuery.GetLastArrivals();
            return View("_LatesArrivals", products);
        }
    }
}
