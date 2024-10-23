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
    public class OrdenProductoProfile : Profile
    {
        public OrdenProductoProfile()
        {
            CreateMap<tb_orden_producto, OrdenProductoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.opr_id)
                )
                .ForMember(
                    dest => dest.Producto,
                    opt => opt.MapFrom(src => src.pro)
                )
                .ForMember(
                    dest => dest.Orden,
                    opt => opt.MapFrom(src => src.ord)
                )
                .ForMember(
                    dest => dest.Cantidad,
                    opt => opt.MapFrom(src => src.opr_cantidad)
                )
                .ForMember(
                    dest => dest.Precio,
                    opt => opt.MapFrom(src => src.opr_precio)
                )
                .ForMember(
                    dest => dest.Total,
                    opt => opt.MapFrom(src => src.opr_total)
                )
                .ForMember(
                    dest => dest.EsActivo,
                    opt => opt.MapFrom(src => src.opr_es_activo)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.opr_fecha_creacion)
                )
                ;

            CreateMap<OrdenProductoDto, tb_orden_producto>()
                .ForMember(
                    dest => dest.opr_id ,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.pro_id,
                    opt => opt.MapFrom(src => src.Producto.Id)
                )
                .ForMember(
                    dest => dest.pro,
                    opt => opt.MapFrom(src => src.Producto)
                )
                .ForMember(
                    dest => dest.ord_id,
                    opt => opt.MapFrom(src => src.Orden.Id)
                )
                .ForMember(
                    dest => dest.opr_cantidad,
                    opt => opt.MapFrom(src => src.Cantidad)
                )
                .ForMember(
                    dest => dest.opr_precio,
                    opt => opt.MapFrom(src => src.Precio)
                )
                .ForMember(
                    dest => dest.opr_total,
                    opt => opt.MapFrom(src => src.Total)
                )
                .ForMember(
                    dest => dest.opr_es_activo,
                    opt => opt.MapFrom(src => src.EsActivo)
                )
                 .ForMember(
                    dest => dest.opr_fecha_creacion,
                    opt => opt.MapFrom(src => src.FechaCreacion)
                )
                ;

            CreateMap<CrearOrdenDetalleRequest, OrdenProductoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForPath(
                    dest => dest.Producto.Id,
                    opt => opt.MapFrom(src => src.IdProducto)
                )
                .ForMember(
                    dest => dest.Cantidad,
                    opt => opt.MapFrom(src => src.Cantidad)
                )
               .ForMember(
                    dest => dest.Precio,
                    opt => opt.MapFrom(src => src.Precio)
                )
               .ForMember(
                    dest => dest.Total,
                    opt => opt.MapFrom(src => src.Total)
                )
                ;
        }
    }
}
