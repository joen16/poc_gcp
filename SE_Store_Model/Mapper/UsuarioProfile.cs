using AutoMapper;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Request;
using SE_Store_Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Mapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<tb_usuario, UsuarioDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.usu_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.usu_nombre)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.usu_email)
                )
                .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(src => src.est)
                )
                .ForMember(
                    dest => dest.Entidad,
                    opt => opt.MapFrom(src => src.ent)
                )
                .ForMember(
                    dest => dest.Rol,
                    opt => opt.MapFrom(src => src.rol)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.usu_fecha_creacion)
                )
                ;


            CreateMap<UsuarioDto, tb_usuario>()
                .ForMember(
                    dest => dest.usu_id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.usu_nombre,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                .ForMember(
                    dest => dest.usu_email,
                    opt => opt.MapFrom(src => src.Email)
                )
                .ForMember(
                    dest => dest.rol_id,
                    opt => opt.MapFrom(src => src.Rol.Id)
                )
                .ForMember(
                    dest => dest.est_id,
                    opt => opt.MapFrom(src => src.Estado.Id)
                )
                .ForMember(
                    dest => dest.ent_id,
                    opt => opt.MapFrom(src => src.Entidad.Id)
                )
                ;

            CreateMap<CrearUsuarioRequest, UsuarioDto>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForMember(
                   dest => dest.Nombre,
                   opt => opt.MapFrom(src => src.Nombre)
               )
               .ForMember(
                   dest => dest.Email,
                   opt => opt.MapFrom(src => src.Email)
               )
               .ForPath(
                   dest => dest.Rol.Id,
                   opt => opt.MapFrom(src => src.IdRol)
               )
               ;

            CreateMap<UsuarioDto, UserProfile>()
               .ForMember(
                   dest => dest.IdUsuario,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForMember(
                   dest => dest.Nombre,
                   opt => opt.MapFrom(src => src.Nombre)
               )
               .ForMember(
                   dest => dest.Username,
                   opt => opt.MapFrom(src => src.Email)
                )
               ;
        }
    }
}
