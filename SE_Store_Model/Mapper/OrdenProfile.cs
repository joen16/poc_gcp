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
    public class OrdenProfile : Profile
    {
        public OrdenProfile()
        {
            CreateMap<tb_orden, OrdenDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.ord_id)
                )
                .ForMember(
                    dest => dest.Entidad,
                    opt => opt.MapFrom(src => src.ent)
                )
                .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(src => src.est)
                )
                .ForMember(
                    dest => dest.Canal,
                    opt => opt.MapFrom(src => src.can)
                )
                .ForMember(
                    dest => dest.Cliente,
                    opt => opt.MapFrom(src => src.cli)
                )
                .ForMember(
                    dest => dest.Direccion,
                    opt => opt.MapFrom(src => src.dir)
                )
                .ForMember(
                    dest => dest.MontoEnvio,
                    opt => opt.MapFrom(src => src.ord_monto_envio)
                )
                .ForMember(
                    dest => dest.MontoSubTotal,
                    opt => opt.MapFrom(src => src.ord_monto_sub_total)
                )
                .ForMember(
                    dest => dest.MontoDescuento,
                    opt => opt.MapFrom(src => src.ord_monto_descuento)
                )
                 .ForMember(
                    dest => dest.MontoTotal,
                    opt => opt.MapFrom(src => src.ord_monto_total)
                )
                .ForMember(
                    dest => dest.MontoPagado,
                    opt => opt.MapFrom(src => src.ord_monto_pagado)
                )
                .ForMember(
                    dest => dest.WpMontoComisionPorc,
                    opt => opt.MapFrom(src => src.ord_wp_monto_comision_porc)
                )
                .ForMember(
                    dest => dest.WpMontoComisionFija,
                    opt => opt.MapFrom(src => src.ord_wp_monto_comision_fija)
                )
                .ForMember(
                    dest => dest.WpMontoIgv,
                    opt => opt.MapFrom(src => src.ord_wp_monto_igv)
                )
                .ForMember(
                    dest => dest.WpMontoComisionTotal,
                    opt => opt.MapFrom(src => src.ord_wp_monto_comision_total)
                )
                .ForMember(
                    dest => dest.WpMontoComercioTotal,
                    opt => opt.MapFrom(src => src.ord_wp_monto_comercio_total)
                )
                .ForMember(
                    dest => dest.Cantidad,
                    opt => opt.MapFrom(src => src.ord_cantidad)
                )
                .ForMember(
                    dest => dest.EsEnvio,
                    opt => opt.MapFrom(src => src.ord_es_envio)
                )
                .ForMember(
                    dest => dest.EsWebPay,
                    opt => opt.MapFrom(src => src.ord_es_web_pay)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.ord_fecha_creacion)
                )
                .ForMember(
                    dest => dest.NumeroOrden,
                    opt => opt.MapFrom(src => src.ord_numero_orden)
                )
                ;

            CreateMap<OrdenDto, tb_orden>()
                .ForMember(
                    dest => dest.ord_id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.ent_id,
                    opt => opt.MapFrom(src => src.Entidad.Id)
                )
               
                .ForMember(
                    dest => dest.est_id,
                    opt => opt.MapFrom(src => src.Estado.Id)
                )
                .ForMember(
                    dest => dest.can_id,
                    opt => opt.MapFrom(src => src.Canal.Id)
                )
                .ForMember(
                    dest => dest.cli_id,
                    opt => opt.MapFrom(src => src.Cliente.Id)
                )
                .ForMember(
                    dest => dest.cli,
                    opt =>
                    {
                        //opt.PreCondition(src => (src.Cliente.Id <= 0));
                        opt.MapFrom(src => src.Cliente);
                    }
                )               
                .ForMember(
                    dest => dest.dir_id,
                    opt => opt.MapFrom(src => src.Direccion.Id)
                )
                .ForMember(
                    dest => dest.dir,
                    opt =>
                    {
                        //opt.PreCondition(src => (src.Direccion.Id <= 0));
                        opt.MapFrom(src => src.Direccion);
                    }
                )
                .ForMember(
                    dest => dest.ord_monto_envio,
                    opt => opt.MapFrom(src => src.MontoEnvio)
                )
                .ForMember(
                    dest => dest.ord_monto_sub_total,
                    opt => opt.MapFrom(src => src.MontoSubTotal)
                )
                .ForMember(
                    dest => dest.ord_monto_descuento,
                    opt => opt.MapFrom(src => src.MontoDescuento)
                )
                 .ForMember(
                    dest => dest.ord_monto_total,
                    opt => opt.MapFrom(src => src.MontoTotal)
                )
                .ForMember(
                    dest => dest.ord_monto_pagado,
                    opt => opt.MapFrom(src => src.MontoPagado)
                )
                .ForMember(
                    dest => dest.ord_wp_monto_comision_porc,
                    opt => opt.MapFrom(src => src.WpMontoComisionPorc)
                )
                .ForMember(
                    dest => dest.ord_wp_monto_comision_fija,
                    opt => opt.MapFrom(src => src.WpMontoComisionFija)
                )
                .ForMember(
                    dest => dest.ord_wp_monto_igv,
                    opt => opt.MapFrom(src => src.WpMontoIgv)
                )
                .ForMember(
                    dest => dest.ord_wp_monto_comision_total,
                    opt => opt.MapFrom(src => src.WpMontoComisionTotal)
                )
                .ForMember(
                    dest => dest.ord_wp_monto_comercio_total,
                    opt => opt.MapFrom(src => src.WpMontoComercioTotal)
                )
                .ForMember(
                    dest => dest.ord_cantidad,
                    opt => opt.MapFrom(src => src.Cantidad)
                )
                .ForMember(
                    dest => dest.ord_es_envio,
                    opt => opt.MapFrom(src => src.EsEnvio)
                )
                .ForMember(
                    dest => dest.ord_es_web_pay,
                    opt => opt.MapFrom(src => src.EsWebPay)
                )
                .ForMember(
                    dest => dest.ord_fecha_creacion,
                    opt => opt.MapFrom(src => src.FechaCreacion)
                )
                .ForMember(
                    dest => dest.tb_orden_producto,
                    opt => opt.MapFrom(src => src.ListOrdenProducto)
                )
                ;

            CreateMap<CrearOrdenRequest, OrdenDto>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForMember(
                   dest => dest.MontoDescuento,
                   opt => opt.MapFrom(src => src.MontoDescuento)
               )
               .ForMember(
                   dest => dest.MontoEnvio,
                   opt => opt.MapFrom(src => src.MontoEnvio)
               )
               .ForMember(
                   dest => dest.MontoTotal,
                   opt => opt.MapFrom(src => src.MontoTotal)
               )
               .ForMember(
                   dest => dest.MontoSubTotal,
                   opt => opt.MapFrom(src => src.MontoSubTotal)
               )
               .ForMember(
                   dest => dest.MontoPagado,
                   opt => opt.MapFrom(src => src.MontoPagado)
               )
               .ForMember(
                   dest => dest.Cantidad,
                   opt => opt.MapFrom(src => src.Cantidad)
               )
               .ForMember(
                   dest => dest.EsEnvio,
                   opt => opt.MapFrom(src => src.EsEnvio)
               )
               .ForMember(
                   dest => dest.EsWebPay,
                   opt => opt.MapFrom(src => src.EsWebPay)
               )
               .ForPath(
                   dest => dest.Canal.Id,
                   opt => opt.MapFrom(src => src.IdCanal)
               )
               .ForPath(
                   dest => dest.Cliente.Codigo,
                   opt => opt.MapFrom(src => src.CodigoCliente)
               )
               .ForPath(
                   dest => dest.Cliente.Nombre,
                   opt => opt.MapFrom(src => src.NombreCliente)
               )
               .ForPath(
                   dest => dest.Cliente.Telefono,
                   opt => opt.MapFrom(src => src.TelefonoCliente)
               )
               .ForPath(
                   dest => dest.Direccion.Id,
                   opt => opt.MapFrom(src => src.IdDireccion)
               )
               .ForPath(
                   dest => dest.Direccion.Region.Id,
                   opt => opt.MapFrom(src => src.IdRegion)
               )
               .ForPath(
                   dest => dest.Direccion.Provincia.Id,
                   opt => opt.MapFrom(src => src.IdProvincia)
               )
               .ForPath(
                   dest => dest.Direccion.Distrito.Id,
                   opt => opt.MapFrom(src => src.IdDistrito)
               )
               .ForPath(
                   dest => dest.Direccion.Agencia.Id,
                   opt => opt.MapFrom(src => src.IdAgencia)
               )
               .ForMember(
                   dest => dest.ListOrdenProducto,
                   opt => opt.MapFrom(src => src.Productos)
               )
               ;


            CreateMap<CrearOrdenBorradorRequest, OrdenDto>()
              .ForMember(
                  dest => dest.Id,
                  opt => opt.MapFrom(src => src.Id)
              )
              .ForMember(
                  dest => dest.MontoDescuento,
                  opt => opt.MapFrom(src => src.MontoDescuento)
              )
              .ForMember(
                  dest => dest.MontoEnvio,
                  opt => opt.MapFrom(src => src.MontoEnvio)
              )
              .ForMember(
                  dest => dest.MontoTotal,
                  opt => opt.MapFrom(src => src.MontoTotal)
              )
              .ForMember(
                  dest => dest.MontoSubTotal,
                  opt => opt.MapFrom(src => src.MontoSubTotal)
              )
              .ForMember(
                  dest => dest.MontoPagado,
                  opt => opt.MapFrom(src => src.MontoPagado)
              )
              .ForMember(
                  dest => dest.Cantidad,
                  opt => opt.MapFrom(src => src.Cantidad)
              )
              .ForMember(
                  dest => dest.EsEnvio,
                  opt => opt.MapFrom(src => src.EsEnvio)
              )
              .ForMember(
                  dest => dest.EsWebPay,
                  opt => opt.MapFrom(src => src.EsWebPay)
              )
              .ForPath(
                  dest => dest.Canal.Id,
                  opt => opt.MapFrom(src => src.IdCanal)
              )
              .ForPath(
                  dest => dest.Cliente.Codigo,
                  opt => opt.MapFrom(src => src.CodigoCliente)
              )
              .ForPath(
                  dest => dest.Cliente.Nombre,
                  opt => opt.MapFrom(src => src.NombreCliente)
              )
              .ForPath(
                  dest => dest.Cliente.Telefono,
                  opt => opt.MapFrom(src => src.TelefonoCliente)
              )
              .ForPath(
                  dest => dest.Direccion.Id,
                  opt => opt.MapFrom(src => src.IdDireccion)
              )
              .ForPath(
                  dest => dest.Direccion.Region.Id,
                  opt => opt.MapFrom(src => src.IdRegion)
              )
              .ForPath(
                  dest => dest.Direccion.Provincia.Id,
                  opt => opt.MapFrom(src => src.IdProvincia)
              )
              .ForPath(
                  dest => dest.Direccion.Distrito.Id,
                  opt => opt.MapFrom(src => src.IdDistrito)
              )
              .ForMember(
                  dest => dest.ListOrdenProducto,
                  opt => opt.MapFrom(src => src.Productos)
              )
              ;
        }
    }
}
