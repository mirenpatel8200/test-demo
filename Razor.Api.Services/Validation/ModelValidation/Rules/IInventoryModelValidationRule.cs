using FluentValidation.Results;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Validation.ModelValidation.Rules
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
