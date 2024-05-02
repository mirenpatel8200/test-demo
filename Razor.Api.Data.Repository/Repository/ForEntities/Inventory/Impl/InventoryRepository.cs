using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Razor.Api.Context;
using Razor.Api.DataAccess.Connection;
using Razor.Api.DataAccess.Repository.Base;
using Razor.Api.DataAccess.Repository.Base.Company;

namespace Razor.Api.DataAccess.Repository.ForEntities.Inventory.Impl
{
    public class InventoryRepository : CompanyRepositoryBase, IInventoryRepository
    {
     
        public IEnumerable<long> GetEnclosedKitInventoryIds(long id)
           => ExecQuery<long>(
            $@"select ItemInventoryId from dbo.F_ItemInventoryKit with (nolock) where ItemInventoryKitHeaderId = {id}");

      
        public Model.Inventory GetInventoryByUid(string uid)
         => ExecQuery<Model.Inventory>(
             $@"select * from search.vw_inventory where inventoryUnique ='{uid}'").FirstOrDefault();

        public IEnumerable<Model.Inventory> GetInventoryByItemInventoryId(IEnumerable<long> ids)
          => ExecQuery<Model.Inventory>(
            $@"select * from search.vw_inventory where inventoryId IN ({string.Join(",", ids)})");


        public InventoryRepository(ICompanyDbConnectionFactory dbConnectionFactory, IExecContext execContext, ILogger<ConfigRepositoryBase> logger) : base(dbConnectionFactory, execContext, logger)
        {
        }
    }
}
