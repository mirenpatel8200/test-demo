using Razor.Api.Context.Models;
using Razor.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Validation.Service.ExecContext
{
    public interface IExecContextValidationService
    {
        IExecContextValidationService Validate(ContextData model);
    }
}
