using AutoMapper;
using SE_Store_Dto;
using SE_Store_Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Mapper
{
    public class RegionProfile: Profile
    {
        public RegionProfile()
        {
            CreateMap<tb_region, RegionDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.reg_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.reg_nombre)
                )
                .ForMember(
                    dest => dest.ListProvincia,
                    opt => opt.MapFrom(src => src.tb_provincia)
                )
                ;

           //CreateMap<List<tb_region>, List<RegionDto>>();
        }
    }
}
