using System.Collections.Generic;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Services
{
    public interface IKitsService
    {
        KitModel GetKitByUid(string uid);

        IEnumerable<InventoryModel> GetEnclosedKitInventory(string uid);

    }
}
