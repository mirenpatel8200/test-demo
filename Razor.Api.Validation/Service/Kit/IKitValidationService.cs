using Razor.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Validation.Service.Kit
{
    public interface IKitValidationService
    {
        IKitValidationService Validate(KitModel kitModel);
    }
}
