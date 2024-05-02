using System.Collections.Generic;

namespace Razor.Api.DataAccess.Repository.ForEntities.Inventory
{
    public interface IInventoryRepository 
    {
        IEnumerable<long> GetEnclosedKitInventoryIds(long id);

        IEnumerable<Model.Inventory> GetInventoryByItemInventoryId(IEnumerable<long> ids);

        Model.Inventory GetInventoryByUid(string uid);
    }
}
