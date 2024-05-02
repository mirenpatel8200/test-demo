using FluentValidation.Results;
using Razor.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Validation.ValidationRule.Kit
{
    public interface IKitsModelValidationRules
    {
        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        ValidationResult Validate(KitModel instance);
    }
}
