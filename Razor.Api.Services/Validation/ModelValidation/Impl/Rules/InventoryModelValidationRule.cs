using FluentValidation;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.ModelValidation.Rules;

namespace Razor.Api.Services.V1.Validation.ModelValidation.Impl.Rules
{
    public class InventoryModelValidationRule : AbstractValidator<InventoryModel>, IInventoryModelValidationRule
    {
        public InventoryModelValidationRule()
        {
            RuleFor(inventory => inventory.ItemMasterId)
                .NotEmpty();

            //To DO: Implement Other validation rules
        }
    }
}
