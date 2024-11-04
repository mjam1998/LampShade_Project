
using ShopManagement.Application.Contracts.ProductCategoryAppContract;
using System.Collections.Generic;
using _0_Freamwork.Domain;


namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long,ProductCategory>
    {



        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel serModel);
       
    }
}
