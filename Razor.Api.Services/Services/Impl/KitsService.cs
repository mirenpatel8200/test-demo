using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Razor.Api.DataAccess.Repository.ForEntities.Inventory;
using Razor.Api.DataAccess.Repository.ForEntities.Kit;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Services.Impl
{
    public class KitsService : IKitsService
    {
        private readonly IKitsRepository _kitsRepository;
        private readonly IInventoryRepository _inventoryRepository;

        private readonly IMapper _mapper;
       
        public KitsService(IKitsRepository kitsRepository, IInventoryRepository inventoryRepository,IMapper mapper)
        {
            _kitsRepository = kitsRepository;
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<InventoryModel> GetEnclosedKitInventory(string uid)
        {
            var kitId = _kitsRepository.GetKitIdByEnclosingInventoryUid(uid).FirstOrDefault();
            var itemInventoryIds = _inventoryRepository.GetEnclosedKitInventoryIds(kitId);
            if (itemInventoryIds.Any())
            {
                return _mapper.Map<IEnumerable<InventoryModel>>(_inventoryRepository.GetInventoryByItemInventoryId(itemInventoryIds));
            }
            return Enumerable.Empty<InventoryModel>();
        }

        public KitModel GetKitByUid(string uid)
        {
            return _mapper.Map<KitModel>(_kitsRepository.GetKitByUid(uid));
        }
    }
}
