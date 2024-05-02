using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Razor.Api.Model;

namespace Razor.Api.Validation.ValidationRule.Kit
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
