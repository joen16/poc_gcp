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
    public class EntidadProfile: Profile
    {
        public EntidadProfile()
        {
            CreateMap<tb_entidad, EntidadDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.ent_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.ent_nombre)
                )
                .ForMember(
                    dest => dest.Codigo,
                    opt => opt.MapFrom(src => src.ent_codigo)
                )
                ;

            CreateMap<EntidadDto, tb_entidad>()
                .ForMember(
                    dest => dest.ent_id ,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.ent_nombre ,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                .ForMember(
                    dest => dest.ent_codigo ,
                    opt => opt.MapFrom(src => src.Codigo)
                )
                ;
        }
    }
}
