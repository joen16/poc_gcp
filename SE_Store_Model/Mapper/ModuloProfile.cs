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
    public class ModuloProfile : Profile
    {
        public ModuloProfile()
        {
            CreateMap<tb_modulo, ModuloDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.mod_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.mod_nombre)
                )
                .ForMember(
                    dest => dest.Orden,
                    opt => opt.MapFrom(src => src.mod_orden)
                )                
            ;

            
        }
    }
}
