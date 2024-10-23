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
    public class ProductoDocumentoProfile: Profile
    {
        public ProductoDocumentoProfile()
        {
            CreateMap<tb_producto_documento, ProductoDocumentoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.pdo_id)
                )
                .ForMember(
                    dest => dest.Documento,
                    opt => opt.MapFrom(src => src.doc)
                )
                .ForMember(
                    dest => dest.Producto,
                    opt => opt.MapFrom(src => src.pro)
                )
                .ForMember(
                    dest => dest.EsActivo,
                    opt => opt.MapFrom(src => src.pdo_es_activo)
                )                
                ; 

            CreateMap<ProductoDocumentoDto, tb_producto_documento>()
                .ForMember(
                    dest => dest.pdo_id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.doc_id,
                    opt => opt.MapFrom(src => src.Documento.Id)
                )
                .ForMember(
                    dest => dest.pro_id,
                    opt => opt.MapFrom(src => src.Producto.Id)
                )
                .ForMember(
                    dest => dest.pdo_es_activo,
                    opt => opt.MapFrom(src => src.EsActivo)
                )
                ;
        }
    }
}
