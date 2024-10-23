using SE_Store_Dto;
using SE_Store_Dto.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service.Interface
{
    public interface IReporteService
    {
        Task<List<ReporteStockCategoriaDto>> GetStockPorCategoriaAsync(long idEntidad);
        Task<List<ReporteStockProductoDto>> GetStockPorProductoAsync(long idEntidad);
    }
}
