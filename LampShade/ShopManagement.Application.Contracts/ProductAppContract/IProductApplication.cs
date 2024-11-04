﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Freamwork.Application;

namespace ShopManagement.Application.Contracts.ProductAppContract
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        OperationResult InStock(long id);
        OperationResult NotInStock(long id);
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
