using ShopManagement.Application.Contracts.ProductCategoryAppContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductCategoryAgg;
using _0_Freamwork.Application;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication:IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name))
                return operation.Failed("رکورد تکراری وارد شده دوباره تلاش کنید.");
            var slug = command.Slug.GenerateSlug();
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation=new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
            {
                return operation.Failed("رکورد یافت نشد.");
            }

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id == command.Id))
            {
                return operation.Failed("رکورد تکراری وارد شده دوباره تلاش کنید.");
            }
            var slug= command.Slug.GenerateSlug();
            productCategory.Edit(command.Name,command.Description,command.Picture
                ,command.PictureAlt,command.PictureTitle,command.Keywords,command.MetaDescription,slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel serModel)
        {
            return _productCategoryRepository.Search(serModel);
        }
    }
}
