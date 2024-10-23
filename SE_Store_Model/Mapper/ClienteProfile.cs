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
    public class ClienteProfile: Profile
    {
        public ClienteProfile()
        {
            CreateMap<tb_cliente, ClienteDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.cli_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.cli_nombre)
                )
                .ForMember(
                    dest => dest.Codigo,
                    opt => opt.MapFrom(src => src.cli_codigo)
                )
                .ForMember(
                    dest => dest.Telefono,
                    opt => opt.MapFrom(src => src.cli_telefono)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.cli_fecha_creacion)
                )
                .ForMember(
                    dest => dest.Entidad,
                    opt => opt.MapFrom(src => src.ent)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.cli_email)
                )
                /*.ForMember(
                    dest => dest.ListOrden,
                    opt => opt.MapFrom(src => src.tb_orden)
                )*/
                ;

            CreateMap<ClienteDto, tb_cliente>()
               .ForMember(
                   dest => dest.cli_id,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForMember(
                   dest => dest.cli_nombre ,
                   opt => opt.MapFrom(src => src.Nombre)
               )
               .ForMember(
                   dest => dest.cli_codigo ,
                   opt => opt.MapFrom(src => src.Codigo)
               )
               .ForMember(
                   dest => dest.cli_telefono ,
                   opt => opt.MapFrom(src => src.Telefono)
               )
               .ForMember(
                   dest => dest.cli_fecha_creacion ,
                   opt => opt.MapFrom(src => src.FechaCreacion)
               )
               .ForMember(
                   dest => dest.ent_id ,
                   opt => opt.MapFrom(src => src.Entidad.Id)
               )
               .ForMember(
                   dest => dest.cli_email,
                   opt => opt.MapFrom(src => src.Email)
               )
               ;
        }
    }
}
