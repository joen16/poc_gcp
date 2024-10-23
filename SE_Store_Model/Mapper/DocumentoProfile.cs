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
    public class DocumentoProfile: Profile
    {
        public DocumentoProfile()
        {
            CreateMap<tb_documento, DocumentoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.doc_id)
                )
                .ForMember(
                    dest => dest.Path,
                    opt => opt.MapFrom(src => src.doc_path)
                )
                .ForMember(
                    dest => dest.TipoDocumento,
                    opt => opt.MapFrom(src => src.tdo)
                )
                .ForMember(
                    dest => dest.Peso,
                    opt => opt.MapFrom(src => src.doc_peso)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.doc_fecha_creacion)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.doc_nombre)
                )
            ;

            CreateMap<DocumentoDto, tb_documento>()
                .ForMember(
                    dest => dest.doc_id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.doc_path,
                    opt => opt.MapFrom(src => src.Path)
                )
                .ForMember(
                    dest => dest.tdo_id,
                    opt => opt.MapFrom(src => src.TipoDocumento.Id)
                )
                .ForMember(
                    dest => dest.doc_peso,
                    opt => opt.MapFrom(src => src.Peso)
                )
                .ForMember(
                    dest => dest.doc_fecha_creacion,
                    opt => opt.MapFrom(src => src.FechaCreacion)
                )
                .ForMember(
                    dest => dest.doc_nombre,
                    opt => opt.MapFrom(src => src.Nombre)
                )
            ;
        }
    }
}
