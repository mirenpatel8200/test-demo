using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Razor.Api.Model;

namespace Razor.Api.Validation.ValidationRule.Inventory
{
    public class InventoryModelValidationRule : AbstractValidator<InventoryModel>, IInventoryModelValidationRule
    {
        public InventoryModelValidationRule()
        {

            RuleFor(Inventory => Inventory.ItemMasterId)
                .NotEmpty();


            //To DO: Implement Other validation rules

        }
    }
}
