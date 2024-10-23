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
    public class ProvinciaProfile: Profile
    {
        public ProvinciaProfile()
        {
            CreateMap<tb_provincia, ProvinciaDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.prv_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.prv_nombre)
                )
                .ForMember(
                    dest => dest.ListDistrito,
                    opt => opt.MapFrom(src => src.tb_distrito)
                )
                ;

            //CreateMap<List<tb_provincia>, List<ProvinciaDto>>();
        }
    }
}
