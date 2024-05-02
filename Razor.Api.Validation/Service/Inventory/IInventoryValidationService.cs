using Razor.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Validation.Service.Inventory
{
    public interface IInventoryValidationService
    {
        IInventoryValidationService Validate(InventoryModel model);
    }
}
