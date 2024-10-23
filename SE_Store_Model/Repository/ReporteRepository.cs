using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ReporteRepository : IReporteRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ReporteRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<ReporteStockCategoriaDto>> GetStockPorCategoriaAsync(long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_producto
               .Include(y => y.tip_id_tallaNavigation)
               .Include(x => x.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_colorNavigation)
               .Include(x => x.tip_id_marcaNavigation)
               .Where(gp => gp.est_id == (int)EstadoProductoEnum.ACTIVO
                   && gp.ent_id == idEntidad)
               .GroupBy(c => new { c.tip_id_categoria,
                   tip_nombre_categoria = c.tip_id_categoriaNavigation.tip_nombre, 
                   c.tip_id_talla,
                   tip_nombre_talla = c.tip_id_tallaNavigation.tip_nombre
               })
               .Select(g => new
               {
                   g.Key.tip_id_categoria,
                   g.Key.tip_nombre_categoria,
                   g.Key.tip_id_talla,
                   g.Key.tip_nombre_talla,
                   SUM = g.Sum(t => t.pro_stock)
               });


            var entity = await result.ToListAsync();
            var listResponse = new List<ReporteStockCategoriaDto>();
            entity.ForEach((grupo) =>
            {
                var index = listResponse.FindIndex(x => x.IdCategoria == grupo.tip_id_categoria);
                if (index >= 0 )
                {
                    var r = listResponse[index];
                    var newDetail = new ReporteStockCategoriaDetalleDto() {
                        Nombre= grupo.tip_nombre_talla,
                        Cantidad = grupo.SUM,
                        IdTalla = grupo.tip_id_talla,
                    };
                    r.Total = (r.Total + grupo.SUM);
                    r.ListTalla.Add(newDetail);
                } else
                {
                    var newItem = new ReporteStockCategoriaDto()
                    {
                        IdCategoria = grupo.tip_id_categoria,
                        NombreCategoria = grupo.tip_nombre_categoria,
                        ListTalla = [ new ReporteStockCategoriaDetalleDto()
                        {
                            Nombre = grupo.tip_nombre_talla,
                            Cantidad = grupo.SUM,
                            IdTalla = grupo.tip_id_talla,
                        }],
                        Total = grupo.SUM                       
                    };
                    listResponse.Add(newItem);
                }

            });
            return listResponse;
        }

        public async Task<List<ReporteStockProductoDto>> GetStockPorProductoAsync(long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_producto
               .Include(y => y.tip_id_tallaNavigation)
               .Include(x => x.tip_id_categoriaNavigation)
               .Include(x => x.tip_id_colorNavigation)
               .Include(x => x.tip_id_marcaNavigation)
               .Include(x => x.tb_producto_documento)
               .ThenInclude(y => y.doc)
               .Where(gp => gp.est_id == (int)EstadoProductoEnum.ACTIVO
                   && gp.ent_id == idEntidad)
               .GroupBy(c => new { 
                   c.grp_id, 
                   c.pro_nombre,
                   c.tb_producto_documento.FirstOrDefault().doc.doc_path, 
                   c.tip_id_talla,
                   tip_nombre_talla = c.tip_id_tallaNavigation.tip_nombre,
                   c.pro_stock })
               .Select(g => new
               {
                   g.Key.grp_id,
                   g.Key.pro_nombre,
                   g.Key.doc_path,
                   g.Key.tip_id_talla,
                   g.Key.tip_nombre_talla,
                   SUM = g.Sum(t => t.pro_stock)
               });


            var entity = await result.ToListAsync();
            var listResponse = new List<ReporteStockProductoDto>();
            entity.ForEach((grupo) =>
            {
                var index = listResponse.FindIndex(x => x.Id == grupo.grp_id);
                if (index >= 0)
                {
                    var r = listResponse[index];
                    var newDetail = new ReporteStockProductoDetalleDto()
                    {
                        Nombre = grupo.tip_nombre_talla,
                        Cantidad = grupo.SUM
                    };
                    r.Total = (r.Total + grupo.SUM);
                    r.ListTalla.Add(newDetail);
                }
                else
                {
                    var newItem = new ReporteStockProductoDto()
                    {
                        Id = grupo.grp_id,
                        Nombre = grupo.pro_nombre,
                        Url = grupo.doc_path,
                        ListTalla = [ new ReporteStockProductoDetalleDto()
                        {
                            IdTalla = grupo.tip_id_talla,
                            Nombre = grupo.tip_nombre_talla,
                            Cantidad = grupo.SUM
                        }],                        
                        Total = grupo.SUM
                    };
                    listResponse.Add(newItem);
                }

            });
            return listResponse;
        }

    }
}
