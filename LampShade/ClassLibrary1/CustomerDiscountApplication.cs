﻿using DiscountManagement.Application.Contract.CustomerDiscountAppContract;
using System;
using System.Collections.Generic;
using _0_Freamwork.Application;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication:ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate&&x.EndDate==command.EndDate.ToGeorgianDateTime()))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var customerDiscount = new CustomerDiscount(command.ProductId,command.DiscountRate,
                command.StartDate.ToGeorgianDateTime(),command.EndDate.ToGeorgianDateTime(),command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.Get(command.Id);
            if (customerDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_customerDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate&& x.Id !=command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            customerDiscount.Edit(command.ProductId, command.DiscountRate,
                command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}
