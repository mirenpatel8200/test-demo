using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using Razor.Api.DataAccess.Repository.ForEntities.Inventory;
using Razor.Api.Model.V1;
using Razor.Api.DataAccess.Repository.ForEntities.Kit;

namespace Razor.Api.Services.V1.Services.Impl
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IKitsRepository _kitsRepository;

        private readonly IMapper _mapper;
       
        public InventoryService(IInventoryRepository inventoryRepository, IKitsRepository kitsRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _kitsRepository = kitsRepository;
            _mapper = mapper;
         }


        public InventoryModel GetInventory(string uid)
        {
            return _mapper.Map<InventoryModel>(_inventoryRepository.GetInventoryByUid(uid));
        }
    }
}
