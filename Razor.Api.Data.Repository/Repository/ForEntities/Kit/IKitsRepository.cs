using System.Collections.Generic;

namespace Razor.Api.DataAccess.Repository.ForEntities.Kit
{
    public interface IKitsRepository 
    {
        /*TODO 3-2: if it is "GetKitByUid(string kitUid)" why does it return multiple kits by a unique id? Looks like should return one on none
         same in the service and the controller
         I am not sure about the posibility to return multiple kits. on that I have used the query that is provided by you.
        */
        Model.Kit GetKitByUid(string kitUid);

        IEnumerable<long> GetKitIdByEnclosingInventoryUid(string uid);
    }
}
