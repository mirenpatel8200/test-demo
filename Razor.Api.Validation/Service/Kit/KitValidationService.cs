using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Razor.Api.Model;
using Razor.Api.Validation.ValidationRule.Kit;

namespace Razor.Api.Validation.Service.Kit
{
    public class KitValidationService : IKitValidationService
    {
        private readonly IKitsModelValidationRules KitsModelValidationRules;
        public KitValidationService(IKitsModelValidationRules kitsModelValidationRules)
        {
            this.KitsModelValidationRules = kitsModelValidationRules;
        }

        public IKitValidationService Validate(KitModel kitModel)
        {
            var validationResult = KitsModelValidationRules.Validate(kitModel);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return this;
        }
    }
}
