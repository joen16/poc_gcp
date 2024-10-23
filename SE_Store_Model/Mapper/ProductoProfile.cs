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
    public class ProductoProfile: Profile
    {
        public ProductoProfile()
        {
            CreateMap<tb_producto, ProductoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.pro_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.pro_nombre)
                )
                .ForMember(
                    dest => dest.Entidad,
                    opt => opt.MapFrom(src => src.ent)
                )
                .ForMember(
                    dest => dest.Talla,
                    opt => opt.MapFrom(src => src.tip_id_tallaNavigation)
                )
                .ForMember(
                    dest => dest.Marca,
                    opt => opt.MapFrom(src => src.tip_id_marcaNavigation)
                )
                .ForMember(
                    dest => dest.Color,
                    opt => opt.MapFrom(src => src.tip_id_colorNavigation)
                )
                .ForMember(
                    dest => dest.Categoria,
                    opt => opt.MapFrom(src => src.tip_id_categoriaNavigation)
                )
                .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(src => src.est)
                )
                .ForMember(
                    dest => dest.Stock,
                    opt => opt.MapFrom(src => src.pro_stock)
                )
                .ForMember(
                    dest => dest.Precio,
                    opt => opt.MapFrom(src => src.pro_precio)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.pro_fecha_creacion)
                )
                ;

            CreateMap<ProductoDto, tb_producto>()
                .ForMember(
                    dest => dest.pro_id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.pro_nombre,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                .ForMember(
                    dest => dest.ent_id,
                    opt => opt.MapFrom(src => src.Entidad.Id)
                )
                .ForMember(
                    dest => dest.tip_id_talla,
                    opt => opt.MapFrom(src => src.Talla.Id)
                )
                .ForMember(
                    dest => dest.tip_id_marca,
                    opt => opt.MapFrom(src => src.Marca.Id)
                )
                .ForMember(
                    dest => dest.tip_id_color,
                    opt => opt.MapFrom(src => src.Color.Id)
                )
                .ForMember(
                    dest => dest.tip_id_categoria,
                    opt => opt.MapFrom(src => src.Categoria.Id)
                )
                .ForMember(
                    dest => dest.est_id,
                    opt => opt.MapFrom(src => src.Estado.Id)
                )
                .ForMember(
                    dest => dest.pro_stock,
                    opt => opt.MapFrom(src => src.Stock)
                )
                .ForMember(
                    dest => dest.pro_precio,
                    opt => opt.MapFrom(src => src.Precio)
                )
                .ForMember(
                    dest => dest.pro_fecha_creacion,
                    opt => opt.MapFrom(src => src.FechaCreacion)
                )
                .ForMember(
                    dest => dest.tb_producto_documento,
                    opt => opt.MapFrom(src => src.ListProductoDocumento)
                )
                ;

            CreateMap<CrearGrupoProductoDetallelRequest, ProductoDto>()
                 .ForMember(
                     dest => dest.Id,
                     opt => opt.MapFrom(src => src.Id)
                 )
                 .ForPath(
                     dest => dest.Talla.Id,
                     opt => opt.MapFrom(src => src.IdTalla)
                 )
                 .ForMember(
                     dest => dest.Stock,
                     opt => opt.MapFrom(src => src.Stock)
                 )
                 ;
        }
    }
}
