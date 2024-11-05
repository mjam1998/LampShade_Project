using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAppContract;
using ShopManagement.Application.Contracts.ProductCategoryAppContract;
using ShopManagement.Application.Contracts.ProductPictureAppContract;


namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<ProductPictureViewModel> ProductPictures;
        public ProductPictureSearchModel SearchModel;
        public SelectList Products;
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;


        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
            ProductPictures = _productPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture();
            command.Products = _productApplication.GetProducts();
            return Partial("./Create",command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Crete(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editProductPicture = _productPictureApplication.GetDetails(id);
            editProductPicture.Products = _productApplication.GetProducts();
            return Partial("Edit", editProductPicture);
        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _productPictureApplication.Remove(id);
          if (result.IsSuccedded)
              return RedirectToPage("./Index");
          Message = result.Message;
          return RedirectToPage("./Index");
        }

        public IActionResult OnGetReStore(long id)
        {
            var result = _productPictureApplication.ReStore(id);
           if (result.IsSuccedded)
               return RedirectToPage("./Index");
           Message = result.Message;
           return RedirectToPage("./Index");
        }
    }
}
