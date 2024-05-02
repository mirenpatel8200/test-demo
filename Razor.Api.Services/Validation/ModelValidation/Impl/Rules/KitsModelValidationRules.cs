using FluentValidation;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.ModelValidation.Rules;

namespace Razor.Api.Services.V1.Validation.ModelValidation.Impl.Rules
{
    public class KitsModelValidationRules : AbstractValidator<KitModel>, IKitsModelValidationRules
    {
        public KitsModelValidationRules()
        {

            RuleFor(kit => kit.ItemMasterId)
                .NotEmpty();

            //To DO: Implement Other validation rules

        }

    }
}
