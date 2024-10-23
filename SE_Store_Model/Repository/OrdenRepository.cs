using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities;
using SE_Store_Dto;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using SE_Store_Model.EF;
using SE_Store_Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository
{
    public class OrdenRepository : IOrdenRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public OrdenRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<OrdenDto> GetByIdAsync(long idOrden, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_orden
               .Include(x => x.tb_orden_producto)
               .ThenInclude(y => y.pro)
               .ThenInclude(y => y.tip_id_tallaNavigation)
               .Include(x => x.tb_orden_producto)
               .ThenInclude(y => y.pro)
               .ThenInclude(y => y.tb_producto_documento.Where(z => z.pdo_es_activo))
               .ThenInclude(y => y.doc)
               .Include(x => x.tb_orden_producto.Where(y => y.opr_es_activo))
               .ThenInclude(y => y.pro)
               .ThenInclude(x => x.tip_id_categoriaNavigation)
               .Include(x => x.tb_orden_producto)
               .ThenInclude(y => y.pro)
               .ThenInclude(x => x.tip_id_colorNavigation)
               .Include(x => x.tb_orden_producto)
               .ThenInclude(y => y.pro)
               .ThenInclude(x => x.tip_id_marcaNavigation)
               .Include(x => x.est)
               .Include(x => x.ent)
               .Include(x => x.cli)
               .Include(x => x.can)
               .Include(x => x.dir)
               .ThenInclude(x => x.reg)
               .Include(x => x.dir)
               .ThenInclude(x => x.prv)
               .Include(x => x.dir)
               .ThenInclude(x => x.dtr)
               .Include(x => x.dir)
               .ThenInclude(x => x.tip_id_agenciaNavigation)
               .Where(or => or.ent_id == idEntidad
               && or.ord_id == idOrden);

            var entity = await result.FirstOrDefaultAsync();

            var ordenDto = this._mapper.Map<OrdenDto>(entity);
            if (ordenDto != null && entity != null)
            {
                ordenDto.ListOrdenProducto = this._mapper.Map<List<OrdenProductoDto>>(entity.tb_orden_producto);

                foreach (var ordenProductoDto in ordenDto.ListOrdenProducto)
                {

                    if (ordenProductoDto.Producto != null)
                    {
                        var op = entity.tb_orden_producto.Where(x => x.opr_id == ordenProductoDto.Id).FirstOrDefault();
                        if (op != null)
                        {
                            ordenProductoDto.Producto.ListProductoDocumento = this._mapper.Map<List<ProductoDocumentoDto>>(op.pro.tb_producto_documento);
                            foreach (var productoDocumentoDto in ordenProductoDto.Producto.ListProductoDocumento)
                            {
                                var pd = op.pro.tb_producto_documento.Where(y => y.pdo_id == productoDocumentoDto.Id).FirstOrDefault();
                                if (pd != null)
                                {
                                    productoDocumentoDto.Documento = this._mapper.Map<DocumentoDto>(pd.doc);
                                }
                            }
                        }
                    }
                }
            }
            return ordenDto;
        }

        public async Task<OrdenDto> GetByNumeroOrdenAsync(long idEntidad, string numeroOrden)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_orden
               .Include(x => x.tb_orden_producto)
               .ThenInclude(y => y.pro)
               .ThenInclude(y => y.tip_id_tallaNavigation)
               .Include(x => x.tb_orden_producto)
               .ThenInclude(y => y.pro)
               .ThenInclude(y => y.tb_producto_documento.Where(z => z.pdo_es_activo))
               .ThenInclude(y => y.doc)
               .Include(x => x.tb_orden_producto.Where(y => y.opr_es_activo))
               .ThenInclude(y => y.pro)
               .ThenInclude(x => x.tip_id_categoriaNavigation)
               .Include(x => x.tb_orden_producto)
               .ThenInclude(y => y.pro)
               .ThenInclude(x => x.tip_id_colorNavigation)
               .Include(x => x.tb_orden_producto)
               .ThenInclude(y => y.pro)
               .ThenInclude(x => x.tip_id_marcaNavigation)
               .Include(x => x.est)
               .Include(x => x.ent)
               .Include(x => x.cli)
               .Include(x => x.can)
               .Include(x => x.dir)
               .ThenInclude(x => x.reg)
               .Include(x => x.dir)
               .ThenInclude(x => x.prv)
               .Include(x => x.dir)
               .ThenInclude(x => x.dtr)
               .Include(x => x.dir)
               .ThenInclude(x => x.tip_id_agenciaNavigation)
               .Where(or => or.ent_id == idEntidad
               && or.ord_numero_orden == numeroOrden
               );

            var entity = await result.FirstOrDefaultAsync();

            var ordenDto = this._mapper.Map<OrdenDto>(entity);
            if (ordenDto != null && entity != null)
            {
                ordenDto.ListOrdenProducto = this._mapper.Map<List<OrdenProductoDto>>(entity.tb_orden_producto);

                foreach (var ordenProductoDto in ordenDto.ListOrdenProducto)
                {

                    if (ordenProductoDto.Producto != null)
                    {
                        var op = entity.tb_orden_producto.Where(x => x.opr_id == ordenProductoDto.Id).FirstOrDefault();
                        if (op != null)
                        {
                            ordenProductoDto.Producto.ListProductoDocumento = this._mapper.Map<List<ProductoDocumentoDto>>(op.pro.tb_producto_documento);
                            foreach (var productoDocumentoDto in ordenProductoDto.Producto.ListProductoDocumento)
                            {
                                var pd = op.pro.tb_producto_documento.Where(y => y.pdo_id == productoDocumentoDto.Id).FirstOrDefault();
                                if (pd != null)
                                {
                                    productoDocumentoDto.Documento = this._mapper.Map<DocumentoDto>(pd.doc);
                                }
                            }
                        }
                    }
                }
            }
            return ordenDto;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerOrdenRequest request, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            DateTime fechaDesde = DateTime.MinValue;
            DateTime fechaHasta = DateTime.MaxValue;
            DateTime.TryParse(request.FechaDesde, out fechaDesde);
            DateTime.TryParse(request.FechaHasta + " 23:59:59", out fechaHasta);


            var result = context.tb_orden
               .Include(x => x.can)
               .Include(x => x.cli)
               .Include(x => x.dir)
               .ThenInclude(x => x.tip_id_agenciaNavigation)
               .Include(x => x.est)
               .Include(x => x.tb_orden_producto)
               .Where(gp => gp.ent_id == idEntidad
               && (gp.ord_fecha_creacion >= fechaDesde)
               && (gp.ord_fecha_creacion <= fechaHasta)
               && (request.IdEstado == 0 ? gp.est_id != (long)EstadoOrdenEnum.ESPERANDO_PAGO  : gp.est_id == request.IdEstado))
               .OrderBy(a => a.ord_id);

            var listEntity = await result
                .Skip((paginado.currentPage - 1) * paginado.regPerPage)
                .Take(paginado.regPerPage)
                .ToListAsync();
            var pages = (decimal)result.Count() / paginado.regPerPage;

            PaginadoResponse response = new PaginadoResponse();
            response.TotalPage = Convert.ToInt32(Math.Ceiling(pages));
            var listOrden = this._mapper.Map<List<OrdenDto>>(listEntity);

            response.Data = listOrden;
            return response;
        }

        public async Task<OrdenDto?> InsertAsync(OrdenDto ordenDto)
        {
            var ordenEntity = this._mapper.Map<tb_orden>(ordenDto);
            //var clienteEntity = this._mapper.Map<tb_cliente>(ordenDto.Cliente);
            var direccionEntity = _unitOfWork.GetRepository<tb_direccion>();
            var ordenRepository = _unitOfWork.GetRepository<tb_orden>();
            var clienteDireccionRepository = _unitOfWork.GetRepository<tb_cliente_direccion>();
            var clienteRepository = _unitOfWork.GetRepository<tb_cliente>();
            var productoRepository = _unitOfWork.GetRepository<tb_producto>();
            var context = _unitOfWork.GetContext();

            //ordenEntity.cli = clienteEntity;
            //ordenEntity.dir = direccionEntity;
            var transacction = _unitOfWork.BeginTransaction();

            try
            {
                if (ordenEntity.cli_id > 0)
                {
                    var clienteBD = await clienteRepository.GetByIdAsync(ordenEntity.cli_id);
                    if (clienteBD != null)
                    {
                        clienteBD.cli_nombre = ordenEntity.cli.cli_nombre;
                        await clienteRepository.UpdateAsync(clienteBD);
                        _unitOfWork.SaveChanges();
                    }
                    ordenEntity.cli = null;
                }
                else
                {
                    var cli = await clienteRepository.InsertAsync(ordenEntity.cli);
                    _unitOfWork.SaveChanges();
                    ordenEntity.cli_id = cli.cli_id;
                    ordenEntity.cli = null;
                }

                if (ordenEntity.dir_id <= 0)
                {
                    var dir = await direccionEntity.InsertAsync(ordenEntity.dir);
                    _unitOfWork.SaveChanges();
                    ordenEntity.dir_id = dir.dir_id;
                    ordenEntity.dir = null;
                }

                var clienteDireccion = context.tb_cliente_direccion
                    .Include(x => x.dir)
                    .Where(x => x.cli_id == ordenEntity.cli_id)
                    .Where(x => x.cld_es_activo)
                    .FirstOrDefault();

                if (clienteDireccion != null && clienteDireccion.dir != null)
                {
                    if (ordenEntity.dir != null
                    && (clienteDireccion.dir.reg_id != ordenEntity.dir.reg_id
                    || clienteDireccion.dir.prv_id != ordenEntity.dir.prv_id
                    || clienteDireccion.dir.dtr_id != ordenEntity.dir.dtr_id
                    || clienteDireccion.dir.tip_id_agencia != ordenEntity.dir.tip_id_agencia))
                    {
                        clienteDireccion.cld_es_activo = false;
                        await clienteDireccionRepository.UpdateAsync(clienteDireccion);

                        var cdEntity = new tb_cliente_direccion()
                        {
                            cli_id = clienteDireccion.cli_id,
                            dir_id = ordenEntity.dir_id ?? 0
                        };
                        await clienteDireccionRepository.InsertAsync(cdEntity);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        ordenEntity.dir = null;
                    }
                }
                else
                {
                    var cdEntity = new tb_cliente_direccion()
                    {
                        cli_id = ordenEntity.cli_id,
                        dir_id = ordenEntity.dir_id ?? 0,
                        cld_es_activo = true
                    };
                    await clienteDireccionRepository.InsertAsync(cdEntity);
                    _unitOfWork.SaveChanges();
                }


                foreach (var op in ordenEntity.tb_orden_producto)
                {
                    var productoEntity = context.tb_producto
                       .Where(gp => gp.pro_id == op.pro_id)
                       .FirstOrDefault();

                    if (productoEntity != null)
                    {
                        productoEntity.pro_stock = op.pro.pro_stock;
                        await productoRepository.UpdateAsync(productoEntity);
                        _unitOfWork.SaveChanges();
                        op.pro = null;
                    }
                }

                var newEntity = await ordenRepository.InsertAsync(ordenEntity);

                _unitOfWork.SaveChanges();
                transacction.Commit();
                ordenDto.Id = newEntity.ord_id;
                return ordenDto;
            }
            catch (Exception ex)
            {
                transacction.Rollback();
                throw;
            }
        }

        public async Task UpdateNumeroOrdenAsync(long idOrden, string numeroOrden)
        {
            var ordenRepository = _unitOfWork.GetRepository<tb_orden>();

            var orden = await ordenRepository.GetByIdAsync(idOrden);
            orden.ord_numero_orden = numeroOrden;
            await ordenRepository.UpdateAsync(orden);
            _unitOfWork.SaveChanges();
        }

        public async Task<OrdenDto?> UpdateAsync(OrdenDto ordenDto)
        {
            var ordenEntityNew = this._mapper.Map<tb_orden>(ordenDto);

            var context = _unitOfWork.GetContext();
            var ordenRepository = _unitOfWork.GetRepository<tb_orden>();
            var productoRepository = _unitOfWork.GetRepository<tb_producto>();
            var ordenProductoRepository = _unitOfWork.GetRepository<tb_orden_producto>();
            var clienteRepository = _unitOfWork.GetRepository<tb_cliente>();
            var clienteDireccionRepository = _unitOfWork.GetRepository<tb_cliente_direccion>();
            var direccionRepository = _unitOfWork.GetRepository<tb_direccion>();

            var transacction = _unitOfWork.BeginTransaction();

            try
            {
                //actualizamos los eliminados
                var listOrdenProductosBD = context.tb_orden_producto
                    .Where(p => p.ord_id == ordenDto.Id)
                    .Where(p => p.opr_es_activo)
                    .AsNoTracking().ToList();

                foreach (var ordenProductoBD in listOrdenProductosBD)
                {
                    if (ordenEntityNew.tb_orden_producto.FirstOrDefault(x => x.opr_id == ordenProductoBD.opr_id) == null)
                    {
                        ordenProductoBD.opr_es_activo = false;
                        await ordenProductoRepository.UpdateAsync(ordenProductoBD);

                        var producto = context.tb_producto
                            .Where(p => p.pro_id == ordenProductoBD.pro_id)
                            .AsNoTracking().FirstOrDefault();

                        if (producto != null)
                        {
                            producto.pro_stock = (producto.pro_stock + ordenProductoBD.opr_cantidad);
                            await productoRepository.UpdateAsync(producto);
                        }
                    }
                }

                //actualizamos los productos
                foreach (var ordenProducEntity in ordenEntityNew.tb_orden_producto)
                {
                    var productoEntity = context.tb_producto
                      .Where(gp => gp.pro_id == ordenProducEntity.pro_id)
                      .FirstOrDefault();

                    if (productoEntity != null)
                    {
                        productoEntity.pro_stock = ordenProducEntity.pro.pro_stock;
                        await productoRepository.UpdateAsync(productoEntity);
                        _unitOfWork.SaveChanges();
                    }

                    ordenProducEntity.ord_id = ordenEntityNew.ord_id;
                    ordenProducEntity.pro = null;
                    if (ordenProducEntity.opr_id > 0)
                    {
                        await ordenProductoRepository.UpdateAsync(ordenProducEntity);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        var opBd = await ordenProductoRepository.InsertAsync(ordenProducEntity);
                        _unitOfWork.SaveChanges();
                        ordenProducEntity.opr_id = opBd.opr_id;
                    }


                }
                // actualizamos la direcion
                if (ordenEntityNew.dir_id > 0)
                {
                    await direccionRepository.UpdateAsync(ordenEntityNew.dir);
                }
                else
                {
                    ordenEntityNew.dir = await direccionRepository.InsertAsync(ordenEntityNew.dir);
                    _unitOfWork.SaveChanges();
                }

                // actualizamos datos de cliente
                if (ordenEntityNew.cli_id > 0)
                {
                    await clienteRepository.UpdateAsync(ordenEntityNew.cli);
                }
                else
                {
                    ordenEntityNew.cli = await clienteRepository.InsertAsync(ordenEntityNew.cli);
                    _unitOfWork.SaveChanges();
                }

                var cdEntityBD = context.tb_cliente_direccion
                    .Include(x => x.dir)
                 .Where(c => c.cli.ent_id == ordenEntityNew.ent_id
                 && c.cli_id == ordenEntityNew.cli_id
                 && c.cld_es_activo).FirstOrDefault();

                if (cdEntityBD != null)
                {
                    if (cdEntityBD.dir.reg_id != ordenDto.Direccion.Region.Id
                        || cdEntityBD.dir.prv_id != ordenDto.Direccion.Provincia.Id
                        || cdEntityBD.dir.dtr_id != ordenDto.Direccion.Distrito.Id
                        || cdEntityBD.dir.tip_id_agencia != ordenDto.Direccion.Agencia.Id)
                    {
                        // anulamos direccion anterior
                        cdEntityBD.cld_es_activo = false;
                        await clienteDireccionRepository.UpdateAsync(cdEntityBD);

                        // creamos nueva direccion
                        var cdEntity = new tb_cliente_direccion()
                        {
                            cli_id = ordenEntityNew.cli.cli_id,
                            dir_id = ordenEntityNew.dir.dir_id,
                            cld_es_activo = true
                        };
                        cdEntity = await clienteDireccionRepository.InsertAsync(cdEntity);
                    }
                }
                else
                {
                    // creamos nueva direccion
                    var cdEntity = new tb_cliente_direccion()
                    {
                        cli_id = ordenEntityNew.cli.cli_id,
                        dir_id = ordenEntityNew.dir.dir_id,
                        cld_es_activo = true
                    };
                    cdEntity = await clienteDireccionRepository.InsertAsync(cdEntity);
                }

                await ordenRepository.UpdateAsync(ordenEntityNew);
                _unitOfWork.SaveChanges();
                transacction.Commit();
            }
            catch (Exception ex)
            {
                transacction.Rollback();
                throw;
            }
            return ordenDto;
        }


        public async Task DeleteAsync(OrdenDto ordenDto)
        {
            var ordenEntityNew = this._mapper.Map<tb_orden>(ordenDto);
            var ordenRepository = _unitOfWork.GetRepository<tb_orden>();
            //var ordenProductoRepository = _unitOfWork.GetRepository<tb_orden_producto>();
            var productoRepository = _unitOfWork.GetRepository<tb_producto>();

            var context = _unitOfWork.GetContext();

            var transacction = _unitOfWork.BeginTransaction();
            try
            {
                var ordenEntity = await context.tb_orden
                    .Include(x => x.tb_orden_producto)
                    .ThenInclude(y => y.pro)
                    .Where(x => x.ord_id == ordenDto.Id)
                    .FirstOrDefaultAsync();

                if (ordenEntity != null)
                {
                    ordenEntity.est_id = ordenDto.Estado.Id;
                    await ordenRepository.UpdateAsync(ordenEntity);
                    _unitOfWork.SaveChanges();

                    foreach (var opEntity in ordenEntity.tb_orden_producto)
                    {
                        var op = ordenEntityNew.tb_orden_producto.FirstOrDefault(x => x.opr_id == opEntity.opr_id);
                        if (op != null)
                        {
                            opEntity.pro.pro_stock = op.pro.pro_stock;
                            await productoRepository.UpdateAsync(opEntity.pro);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }

                transacction.Commit();
            }
            catch (Exception ex)
            {
                transacction.Rollback();
                throw;
            }
        }


        public async Task UpdateEstadoAsync(long idOrden, long idEstado)
        {
            var context = _unitOfWork.GetContext();
            var ordenRepository = _unitOfWork.GetRepository<tb_orden>();

            var ordenEntity = context.tb_orden
                     .Where(p => p.ord_id == idOrden)
                     .AsNoTracking().FirstOrDefault();

            if (ordenEntity != null)
            {
                ordenEntity.est_id = idEstado;
                await ordenRepository.UpdateAsync(ordenEntity);
                _unitOfWork.SaveChanges();
            }
        }

    }
}
