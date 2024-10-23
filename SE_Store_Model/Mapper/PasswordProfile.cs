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
    public class PasswordProfile: Profile
    {
        public PasswordProfile()
        {
            CreateMap<tb_password, PasswordDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.pwd_id)
                )
                .ForMember(
                    dest => dest.Valor,
                    opt => opt.MapFrom(src => src.pwd_valor)
                )
                .ForMember(
                    dest => dest.EsActivo,
                    opt => opt.MapFrom(src => src.pwd_es_activo)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.pwd_fecha_creacion)
                )
                .ForMember(
                    dest => dest.Usuario,
                    opt => opt.MapFrom(src => src.usu)
                )
                ;

            CreateMap<PasswordDto, tb_password>()
                 .ForMember(
                     dest => dest.pwd_id ,
                     opt => opt.MapFrom(src => src.Id)
                 )
                 .ForMember(
                     dest => dest.pwd_valor ,
                     opt => opt.MapFrom(src => src.Valor)
                 )
                 .ForMember(
                     dest => dest.pwd_es_activo ,
                     opt => opt.MapFrom(src => src.EsActivo)
                 )
                 .ForMember(
                     dest => dest.pwd_fecha_creacion ,
                     opt => opt.MapFrom(src => src.FechaCreacion)
                 )
                 .ForMember(
                     dest => dest.usu ,
                     opt => opt.MapFrom(src => src.Usuario)
                 )
                 .ForPath(
                     dest => dest.usu_id,
                     opt => opt.MapFrom(src => src.Usuario.Id)
                 )
                 ;
        }
    }
}
