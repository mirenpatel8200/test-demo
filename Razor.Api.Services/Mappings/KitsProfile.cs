using AutoMapper;
using Razor.Api.DataAccess.Repository.ForEntities.Kit.Model;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Mappings
{
    public class KitsProfile : Profile
    {
        public KitsProfile()
        {
            CreateMap<Kit, KitModel>()
                .ReverseMap();
        }
    }
}
