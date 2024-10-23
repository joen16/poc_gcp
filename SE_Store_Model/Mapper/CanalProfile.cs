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
    public class CanalProfile : Profile
    {
        public CanalProfile()
        {
            CreateMap<tb_canal, CanalDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.can_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.can_nombre)
                )
                ;
            CreateMap<CanalDto, tb_canal>()
                .ForMember(
                    dest => dest.can_id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.can_nombre ,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                ;
        }
    }
}
