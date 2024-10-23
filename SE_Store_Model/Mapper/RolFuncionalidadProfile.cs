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
    public class RolFuncionalidadProfile : Profile
    {
        public RolFuncionalidadProfile()
        {
            CreateMap<tb_rol_funcionalidad, RolFuncionalidadDto>()
                .ForMember(
                    dest => dest.Rol,
                    opt => opt.MapFrom(src => src.rol)
                )
                .ForMember(
                    dest => dest.Funcionalidad,
                    opt => opt.MapFrom(src => src.fun)
                )                
            ;

            
        }
    }
}
