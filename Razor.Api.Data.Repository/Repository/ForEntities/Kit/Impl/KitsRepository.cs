using System.Collections.Generic;
using Razor.Api.Context;
using Razor.Api.DataAccess.Connection;
using Razor.Api.DataAccess.Repository.Base.Company;
using System.Linq;
using Microsoft.Extensions.Logging;
using Razor.Api.DataAccess.Repository.Base;

namespace Razor.Api.DataAccess.Repository.ForEntities.Kit.Impl
{
    public class KitsRepository : CompanyRepositoryBase, IKitsRepository
    {
        public Model.Kit GetKitByUid(string kitUid)
            => ExecQuery<Model.Kit>($@"select fkh.Id as KitId,
                                    fkh.ItemInventoryId	as KitInventoryId,
                                    fii.ITEM_INVENTORY_UNIQUE_ID as KitUid,
                                    sku.itemId as KitSkuId,
                                    fi.LONG_DESC as KitLongDescr,
                                    fi.ITEM_DESC as KitDescr,
                                    fi.IS_QUALIFIED as IsQualified,
                                    fi.IS_UNIQUE as IsUnique,
                                    sku.sellingPrice as Price,
                                    sku.onHand as QtyOnHand,
                                    sku.Available as QtyAvailable,
                                    sku.itemMasterNumber,
                                    sku.itemMasterId,
	                                sku.itemMasterTitle,
	                                sku.itemMasterQualifielTitle,
	                                sku.manufacturerId,
	                                sku.manufacturerCd,
	                                sku.conditionId,
	                                sku.conditionCd,
	                                sku.conditionDesc,
	                                sku.itemTypeCd,
	                                sku.itemTypeDesc,
	                                sku.itemMasterPrimaryCategoryName
                            from dbo.F_ItemInventoryKitHeader fkh with (nolock)
                            inner join dbo.f_item_inventory   fii with (nolock)
	                            on fii.item_inventory_id = fkh.ItemInventoryId
                            inner join dbo.f_item             fi with (nolock)
	                            on fi.item_id = fkh.ItemId
                            inner join search.vw_sku          sku
	                            on sku.itemId = fi.item_id
                            where fii.ITEM_INVENTORY_UNIQUE_ID ='{kitUid}'").FirstOrDefault();


        public IEnumerable<long> GetKitIdByEnclosingInventoryUid(string uid)
           => ExecQuery<long>(
           $@"select
                fkh.Id as KitId
            from dbo.F_ItemInventoryKitHeader   fkh with (nolock)
            inner join dbo.f_item_inventory     fii with (nolock)
                on fii.item_inventory_id = fkh.ItemInventoryId
            where fii.ITEM_INVENTORY_UNIQUE_ID ='{uid}'
              and fii.IS_INACTIVE = 0
              and IS_DELETED = 0");
        

        public KitsRepository(ICompanyDbConnectionFactory dbConnectionFactory, IExecContext execContext, ILogger<ConfigRepositoryBase> logger) : base(dbConnectionFactory, execContext, logger)
        {
        }
    }
}
