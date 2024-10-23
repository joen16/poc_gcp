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
    public class TipoDocumentoProfile: Profile
    {
        public TipoDocumentoProfile()
        {
            CreateMap<tb_tipo_documento, TipoDocumentoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.tdo_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.tdo_nombre)
                )
                .ForMember(
                    dest => dest.Path,
                    opt => opt.MapFrom(src => src.tdo_path)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.tdo_fecha_creacion)
                )
                .ForMember(
                    dest => dest.ListDocumento,
                    opt => opt.MapFrom(src => src.tb_documento)
                )
                .ReverseMap();
            ;
        }
    }
}
