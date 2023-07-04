using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Razor.Api.Model;
using Razor.Api.Validation.ValidationRule.Inventory;

namespace Razor.Api.Validation.Service.Inventory
{
    public class InventoryValidationService : IInventoryValidationService
    {
        private readonly IInventoryModelValidationRule InventoryModelValidationRule;
        public InventoryValidationService(IInventoryModelValidationRule inventoryModelValidationRule)
        {
            this.InventoryModelValidationRule = inventoryModelValidationRule;
        }

        public IInventoryValidationService Validate(InventoryModel model)
        {
            var validationResult = InventoryModelValidationRule.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return this;
        }
    }
}
