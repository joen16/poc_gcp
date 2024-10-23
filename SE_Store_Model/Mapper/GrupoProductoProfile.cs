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
    public class GrupoProductoProfile : Profile
    {
        public GrupoProductoProfile()
        {
            CreateMap<tb_grupo_producto, GrupoProductoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.grp_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.grp_nombre)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.grp_nombre)
                )
                .ForMember(
                    dest => dest.Precio,
                    opt => opt.MapFrom(src => src.grp_precio)
                )
                .ForMember(
                    dest => dest.ListProducto,
                    opt => opt.MapFrom(src => src.tb_producto)
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
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.grp_fecha_creacion)
                )
                ;

            CreateMap<GrupoProductoDto, tb_grupo_producto>()
                .ForMember(
                    dest => dest.grp_id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.grp_nombre,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                .ForMember(
                    dest => dest.grp_precio,
                    opt => opt.MapFrom(src => src.Precio)
                )
                .ForMember(
                    dest => dest.ent_id,
                    opt => opt.MapFrom(src => src.Entidad.Id)
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
                    dest => dest.grp_fecha_creacion,
                    opt => opt.MapFrom(src => src.FechaCreacion)
                )
                .ForMember(
                    dest => dest.tb_producto,
                    opt => opt.MapFrom(src => src.ListProducto)
                )
                ;
            CreateMap<CrearGrupoProductoRequest, GrupoProductoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                .ForMember(
                    dest => dest.Precio,
                    opt => opt.MapFrom(src => src.Precio)
                )
                .ForMember(
                    dest => dest.ListProducto,
                    opt => opt.MapFrom(src => src.Productos)
                )
                .ForPath(
                    dest => dest.Marca.Id,
                    opt => opt.MapFrom(src => src.IdMarca)
                )
                .ForPath(
                    dest => dest.Color.Id,
                    opt => opt.MapFrom(src => src.IdColor)
                )
                .ForPath(
                    dest => dest.Categoria.Id,
                    opt => opt.MapFrom(src => src.IdCategoria)
                )
                ;
        }
    }
}
