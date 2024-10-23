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
    public class FuncionalidadProfile : Profile
    {
        public FuncionalidadProfile()
        {
            CreateMap<tb_funcionalidad, FuncionalidadDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.fun_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.fun_nombre)
                )
                .ForMember(
                    dest => dest.Path,
                    opt => opt.MapFrom(src => src.fun_path)
                )
                .ForMember(
                    dest => dest.Orden,
                    opt => opt.MapFrom(src => src.fun_orden)
                )
                .ForMember(
                    dest => dest.Icon,
                    opt => opt.MapFrom(src => src.fun_icon)
                )
                .ForMember(
                    dest => dest.Modulo,
                    opt => opt.MapFrom(src => src.mod)
                )
            ;

            
        }
    }
}
