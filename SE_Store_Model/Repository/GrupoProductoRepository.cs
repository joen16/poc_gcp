using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using SE_Store_Model.EF;
using SE_Store_Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository
{
    public class GrupoProductoRepository : IGrupoProductoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GrupoProductoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<GrupoProductoDto> GetByIdAsync(long idGrupoProducto, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_grupo_producto
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_tallaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tb_producto_documento)
               .ThenInclude(y => y.doc)
               .Include(x => x.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_colorNavigation)
               .Include(x => x.tip_id_marcaNavigation)
               .Include(x => x.est)
               .Include(x => x.ent)
               .Where(gp => gp.ent_id == idEntidad
               && gp.grp_id == idGrupoProducto);

            var entity = await result.FirstOrDefaultAsync();

            var grupo = this._mapper.Map<GrupoProductoDto>(entity);
            grupo.ListProducto.ForEach((producto) =>
            {
                var productoEntity = entity?.tb_producto.Where(x => x.pro_id == producto.Id).FirstOrDefault();

                if (productoEntity != null)
                {
                    var listPd = productoEntity.tb_producto_documento.Where(l => l.pdo_es_activo).ToList();
                    producto.ListProductoDocumento = this._mapper.Map<List<ProductoDocumentoDto>>(listPd);
                }
            });
            return grupo;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerGrupoProductoRequest request, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_grupo_producto
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_tallaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tb_producto_documento)
               .ThenInclude(y => y.doc)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_colorNavigation)
               .Include(x => x.tip_id_marcaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_marcaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_colorNavigation)
               .Include(x => x.est)

               .Where(gp => gp.est_id == (int)EstadoProductoEnum.ACTIVO
               && gp.ent_id == idEntidad
               && (request.IdMarca == 0 || gp.tip_id_marca == request.IdMarca)
               && (request.IdColor == 0 || gp.tip_id_color == request.IdColor)
               && (request.IdCategoria == 0 || gp.tip_id_categoria == request.IdCategoria))
               .OrderBy(a => a.grp_id);



            var listEntity = await result
                .Skip((paginado.currentPage - 1) * paginado.regPerPage)
                .Take(paginado.regPerPage)
                .ToListAsync();
            var pages = (decimal)result.Count() / paginado.regPerPage;

            PaginadoResponse response = new PaginadoResponse();
            response.TotalPage = Convert.ToInt32(Math.Ceiling(pages));
            var listGrupoProducto = this._mapper.Map<List<GrupoProductoDto>>(listEntity);
            listGrupoProducto.ForEach(grupo =>
            {
                grupo.ListProducto.ForEach((producto) =>
                {
                    var grupoEntity = listEntity.Where(x => x.grp_id == grupo.Id).FirstOrDefault();
                    var productoEntity = grupoEntity?.tb_producto.Where(x => x.pro_id == producto.Id).FirstOrDefault();

                    if (productoEntity != null)
                    {
                        var listPd = productoEntity.tb_producto_documento.Where(l => l.pdo_es_activo).ToList();
                        producto.ListProductoDocumento = this._mapper.Map<List<ProductoDocumentoDto>>(listPd);
                    }
                });
            });
            response.Data = listGrupoProducto;
            return response;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerGrupoProductFilterRequest request, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_grupo_producto
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_tallaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tb_producto_documento)
               .ThenInclude(y => y.doc)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_colorNavigation)
               .Include(x => x.tip_id_marcaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_marcaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_colorNavigation)
               .Include(x => x.est)

               .Where(gp => gp.est_id == (int)EstadoProductoEnum.ACTIVO
               && gp.ent_id == idEntidad
               && (!request.IdMarca.Any() || request.IdMarca.Contains(gp.tip_id_marca))
               && (!request.IdColor.Any() || request.IdColor.Contains(gp.tip_id_color))
               && (!request.IdCategoria.Any() || request.IdCategoria.Contains(gp.tip_id_categoria)))
               .OrderBy(a => a.grp_id);



            var listEntity = await result
                .Skip((paginado.currentPage - 1) * paginado.regPerPage)
                .Take(paginado.regPerPage)
                .ToListAsync();
            var pages = (decimal)result.Count() / paginado.regPerPage;

            PaginadoResponse response = new PaginadoResponse();
            response.TotalPage = Convert.ToInt32(Math.Ceiling(pages));
            var listGrupoProducto = this._mapper.Map<List<GrupoProductoDto>>(listEntity);
            listGrupoProducto.ForEach(grupo =>
            {
                grupo.ListProducto.ForEach((producto) =>
                {
                    var grupoEntity = listEntity.Where(x => x.grp_id == grupo.Id).FirstOrDefault();
                    var productoEntity = grupoEntity?.tb_producto.Where(x => x.pro_id == producto.Id).FirstOrDefault();

                    if (productoEntity != null)
                    {
                        var listPd = productoEntity.tb_producto_documento.Where(l => l.pdo_es_activo).ToList();
                        producto.ListProductoDocumento = this._mapper.Map<List<ProductoDocumentoDto>>(listPd);
                    }
                });
            });
            response.Data = listGrupoProducto;
            return response;
        }


        public async Task<GrupoProductoDto?> InsertAsync(GrupoProductoDto grupoProducto)
        {
            var Entity = this._mapper.Map<tb_grupo_producto>(grupoProducto);

            var grupoProductoRepository = _unitOfWork.GetRepository<tb_grupo_producto>();
            var newEntity = await grupoProductoRepository.InsertAsync(Entity);
            _unitOfWork.SaveChanges();

            grupoProducto.Id = newEntity.grp_id;

            return grupoProducto;
        }

        public async Task<GrupoProductoDto?> UpdateAsync(GrupoProductoDto grupoProducto)
        {
            var grupoProductoEntityNew = this._mapper.Map<tb_grupo_producto>(grupoProducto);
            var context = _unitOfWork.GetContext();
            var grupoProductoRepository = _unitOfWork.GetRepository<tb_grupo_producto>();
            var productoRepository = _unitOfWork.GetRepository<tb_producto>();
            var productoDocumentoRepository = _unitOfWork.GetRepository<tb_producto_documento>();

            var transacction = _unitOfWork.BeginTransaction();

            try
            {
                //obtener productos relacionados
                var listProductosEntityBD = context.tb_producto
                    .Include(x => x.tb_producto_documento.Where(pd => pd.pdo_es_activo))
                    .Where(p => p.grp_id == grupoProducto.Id)
                    .AsNoTracking().ToList();

                foreach (var productoEntityBD in listProductosEntityBD)
                {
                    if (grupoProductoEntityNew.tb_producto.FirstOrDefault(x => x.pro_id == productoEntityBD.pro_id) == null)
                    {
                        productoEntityBD.est_id = (int)EstadoProductoEnum.ELIMINADO;
                        await productoRepository.UpdateAsync(productoEntityBD);
                    }

                    var docDto = grupoProductoEntityNew.tb_producto.Select(x => x.tb_producto_documento.FirstOrDefault()).FirstOrDefault();

                    foreach (var docBD in productoEntityBD.tb_producto_documento)
                    {
                        if (docBD != null && docDto != null)
                        {
                            if (docDto.doc_id > 0 && docBD.doc_id != docDto.doc_id)
                            {
                                var listDocEntityBD = context.tb_producto_documento
                                .Where(p => p.doc_id == docBD.doc_id && p.pro_id == productoEntityBD.pro_id
                                && p.pdo_es_activo)
                                .AsNoTracking().ToList();

                                foreach (var docEntityBD in listDocEntityBD)
                                {
                                    docEntityBD.pdo_es_activo = false;
                                    await productoDocumentoRepository.UpdateAsync(docEntityBD);
                                }


                            }
                        }
                    }
                }
                foreach (var producEntity in grupoProductoEntityNew.tb_producto)
                {
                    foreach (var docEntity in producEntity.tb_producto_documento)
                    {
                        var listProductoDocumento = listProductosEntityBD.Where(w => w.pro_id == producEntity.pro_id).SelectMany(x => x.tb_producto_documento);
                        if (listProductoDocumento.Any())
                        {
                            var docDto = listProductoDocumento.Where(x => x.doc_id == docEntity.doc_id).FirstOrDefault();
                            //var docDto = listProductosEntityBD.Select(x => x.tb_producto_documento.FirstOrDefault(x => x.pro_id == producEntity.pro_id && x.doc_id == docEntity.doc_id)).FirstOrDefault();

                            if (docDto == null)
                            {
                                await productoDocumentoRepository.InsertAsync(docEntity);

                            }
                        }
                    }
                }


                foreach (var item in grupoProductoEntityNew.tb_producto)
                {
                    if (item.pro_id > 0)
                    {
                        await productoRepository.UpdateAsync(item);
                    }
                    else
                    {
                        await productoRepository.InsertAsync(item);
                    }
                }

                await grupoProductoRepository.UpdateAsync(grupoProductoEntityNew);
                _unitOfWork.SaveChanges();
                transacction.Commit();
            }
            catch (Exception ex)
            {
                transacction.Rollback();
                throw;
            }
            return grupoProducto;
        }


        public async Task DeleteAsync(long idGrupoProducto)
        {
            var grupoProductoRepository = _unitOfWork.GetRepository<tb_grupo_producto>();

            var grupoProductoEntity = await grupoProductoRepository.GetByIdAsync(idGrupoProducto);
            if (grupoProductoEntity != null)
            {
                grupoProductoEntity.est_id = (long)EstadoProductoEnum.ELIMINADO;
                await grupoProductoRepository.UpdateAsync(grupoProductoEntity);
            }

            _unitOfWork.SaveChanges();
        }

        public async Task<List<GrupoProductoDto>> GetPopularAsync(long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var resultIds = context.tb_orden_producto
               .Where(gp => gp.pro.est_id == (int)EstadoProductoEnum.ACTIVO
               && gp.ord.est_id == (int)EstadoOrdenEnum.TERMINADO
                    && gp.opr_es_activo
                   && gp.pro.ent_id == idEntidad)
               .GroupBy(c => new
               {
                   grp_id = c.pro.grp_id,
               })
               .Select(g => new
               {
                   g.Key.grp_id,
                   SUM = g.Sum(t => t.opr_cantidad)
               })
               .OrderByDescending(x => x.SUM)
               .Take(40);

            var listIds =await resultIds.ToListAsync();
            var ids = listIds.Select(x => x.grp_id).ToList();

            if(ids.Count < 8)
            {
                ids.Add(new Random().Next(1, 10));
                ids.Add(new Random().Next(11, 20));
                ids.Add(new Random().Next(21, 30));
                ids.Add(new Random().Next(31, 40));
                ids.Add(new Random().Next(41, 50));
                ids.Add(new Random().Next(51, 60));
                ids.Add(new Random().Next(61, 70));
                ids.Add(new Random().Next(71, 80));
            }

            var result = context.tb_grupo_producto
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_tallaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tb_producto_documento)
               .ThenInclude(y => y.doc)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_colorNavigation)
               .Include(x => x.tip_id_marcaNavigation)
               .Include(x => x.est)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_marcaNavigation)
               .Include(x => x.tb_producto)
               .ThenInclude(y => y.tip_id_colorNavigation)
               .Include(x => x.est)
               .Where(gp => gp.est_id == (int)EstadoProductoEnum.ACTIVO
               && gp.ent_id == idEntidad)
               .Where(x=> x.tb_producto.Any(i=> ids.Contains(i.pro_id)))
               .Take(8)
               .OrderBy(a => a.grp_id);

            var listEntity = await result.ToListAsync();

            var listGrupoProducto = this._mapper.Map<List<GrupoProductoDto>>(listEntity);
            listGrupoProducto.ForEach(grupo =>
            {
                grupo.ListProducto.ForEach((producto) =>
                {
                    var grupoEntity = listEntity.Where(x => x.grp_id == grupo.Id).FirstOrDefault();
                    var productoEntity = grupoEntity?.tb_producto.Where(x => x.pro_id == producto.Id).FirstOrDefault();

                    if (productoEntity != null)
                    {
                        var listPd = productoEntity.tb_producto_documento.Where(l => l.pdo_es_activo).ToList();
                        producto.ListProductoDocumento = this._mapper.Map<List<ProductoDocumentoDto>>(listPd);
                    }
                });
            });

            return listGrupoProducto;
        }

    }
}
