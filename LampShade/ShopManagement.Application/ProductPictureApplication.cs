using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.ProductPictureAppContract;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagement.Application
{
    public class ProductPictureApplication:IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository,IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Crete(CreateProductPicture command)
        {
            var operation = new OperationResult();
           // if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
              //  return operation.Failed(ApplicationMessages.DuplicatedRecord);
              var product=_productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{product.Category.Slug}/{product.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
            var productPicture = new ProductPicture(command.ProductId,pictureName, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
          var operation=new OperationResult();
          var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);
            if (productPicture == null)
              return operation.Failed(ApplicationMessages.RecordNotFound);
            // if (_productPictureRepository.Exists(x =>
            //  x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id != command.Id))
            
            var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
          
          productPicture.Edit(command.ProductId,pictureName,command.PictureAlt,command.PictureTitle);
          _productPictureRepository.SaveChanges();
          return operation.Succedded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
         
               
            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }
        public OperationResult ReStore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);


            productPicture.ReStore();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
