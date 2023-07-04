using System;
using System.Collections.Generic;
using System.Text;
using Razor.Api.Context.Models;
using FluentValidation.Results;

namespace Razor.Api.Validation.ValidationRule.ExecContext
{
    public interface IExecContextValidationRule 
    {
        ValidationResult Validate(ContextData data);
    }
}
