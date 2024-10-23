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
    public class RolProfile: Profile
    {
        public RolProfile()
        {
            CreateMap<tb_rol, RolDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.rol_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.rol_nombre)
                )
                ;

            CreateMap<RolDto, tb_rol>()
                .ForMember(
                    dest => dest.rol_id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.rol_nombre,
                    opt => opt.MapFrom(src => src.Nombre)
                );

        }
    }
}
