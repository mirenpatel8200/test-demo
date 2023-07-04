using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Razor.Api.Context.Models;
using Razor.Api.Model;
using Razor.Api.Validation.ValidationRule.ExecContext;

namespace Razor.Api.Validation.Service.ExecContext
{
    public class ExecContextValidationService : IExecContextValidationService
    {
        private readonly IExecContextValidationRule ExecContextValidationRule;
        public ExecContextValidationService(IExecContextValidationRule execContextValidationRule)
        {
            this.ExecContextValidationRule = execContextValidationRule;
        }
        public IExecContextValidationService Validate(ContextData model)
        {
            var validationResult = ExecContextValidationRule.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return this;
        }
    }
}
