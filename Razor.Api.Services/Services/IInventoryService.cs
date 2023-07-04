using System.Collections.Generic;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Services
{
    public interface IInventoryService
    {
        InventoryModel GetInventory(string uid);
    }
}
