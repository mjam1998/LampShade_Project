

using System.Collections.Generic;

namespace _01_LampShadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetDetails(string slug);
        List<ProductQueryModel> GetLastArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
