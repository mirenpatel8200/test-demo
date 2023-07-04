using AutoMapper;
using Razor.Api.DataAccess.Repository.ForEntities.Inventory.Model;
using Razor.Api.Model.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Services.V1.Mappings
{
    public class SubstituteProfile : Profile
    {
        public SubstituteProfile()
        {
            CreateMap<Substitute, SubstituteModel>().ReverseMap();
        }
    }
}
