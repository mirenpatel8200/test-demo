using FluentValidation.Results;
using Razor.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Validation.ValidationRule.Inventory
{
    public interface IInventoryModelValidationRule 
    {
        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        ValidationResult Validate(InventoryModel instance);
    }
}
