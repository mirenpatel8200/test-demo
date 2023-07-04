using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Validation.ModelValidation
{
    public interface IInventoryValidationService
    {
        IInventoryValidationService Validate(InventoryModel model);
    }
}
