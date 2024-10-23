using AutoMapper;
using SE_Store_Dto;
using SE_Store_Dto.Request;
using SE_Store_Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Mapper
{
    public class TipoProfile: Profile
    {
        public TipoProfile()
        {
            CreateMap<tb_tipo, TipoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.tip_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.tip_nombre)
                )
                .ForMember(
                    dest => dest.EsActivo,
                    opt => opt.MapFrom(src => src.tip_es_activo)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.tip_fecha_creacion)
                )
                .ForMember(
                    dest => dest.Entidad,
                    opt => opt.MapFrom(src => src.ent)
                )
                .ForMember(
                    dest => dest.Clasificacion,
                    opt => opt.MapFrom(src => src.cti)
                )
                ;

            CreateMap<TipoDto, tb_tipo>()
                .ForMember(
                    dest => dest.tip_id ,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.tip_nombre ,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                .ForMember(
                    dest => dest.tip_es_activo ,
                    opt => opt.MapFrom(src => src.EsActivo)
                )
                .ForMember(
                    dest => dest.tip_fecha_creacion ,
                    opt => opt.MapFrom(src => src.FechaCreacion)
                )
                .ForMember(
                    dest => dest.ent ,
                    opt => opt.MapFrom(src => src.Entidad)
                )
                .ForMember(
                    dest => dest.ent_id,
                    opt => opt.MapFrom(src => src.Entidad.Id)
                )
                .ForMember(
                    dest => dest.cti ,
                    opt => opt.MapFrom(src => src.Clasificacion)
                )
                .ForMember(
                    dest => dest.cti_id,
                    opt => opt.MapFrom(src => src.Clasificacion.Id)
                )
                ;

            CreateMap<CrearTipoRequest, TipoDto>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForPath(
                   dest => dest.Clasificacion.Id,
                   opt => opt.MapFrom(src => src.IdClasificacion)
               )
               .ForMember(
                   dest => dest.Nombre,
                   opt => opt.MapFrom(src => src.Nombre)
               )
               ;
        }
    }
}
