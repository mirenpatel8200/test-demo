using AutoMapper;
using Razor.Api.DataAccess.Repository.ForEntities.Inventory.Model;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Mappings
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<Inventory, InventoryModel>()
                .ReverseMap();
        }
    }
}
