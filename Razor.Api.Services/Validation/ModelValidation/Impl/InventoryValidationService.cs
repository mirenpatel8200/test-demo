using FluentValidation;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.ModelValidation.Rules;

namespace Razor.Api.Services.V1.Validation.ModelValidation.Impl
{
    public class InventoryValidationService : IInventoryValidationService
    {
        private readonly IInventoryModelValidationRule _inventoryModelValidationRule;
        public InventoryValidationService(IInventoryModelValidationRule inventoryModelValidationRule)
        {
            this._inventoryModelValidationRule = inventoryModelValidationRule;
        }

        public IInventoryValidationService Validate(InventoryModel model)
        {
            var validationResult = _inventoryModelValidationRule.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return this;
        }
    }
}
