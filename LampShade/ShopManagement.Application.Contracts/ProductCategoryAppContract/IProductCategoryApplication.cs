using System.Collections.Generic;
using _0_Freamwork.Application;


namespace ShopManagement.Application.Contracts.ProductCategoryAppContract
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
         EditProductCategory GetDetails(long id);
         List<ProductCategoryViewModel> Search(ProductCategorySearchModel serModel);
    }
}
