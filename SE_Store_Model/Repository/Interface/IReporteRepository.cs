using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using SE_Store_Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository.Interface
{
    public interface IReporteRepository
    {
        Task<List<ReporteStockCategoriaDto>> GetStockPorCategoriaAsync(long idEntidad);
        Task<List<ReporteStockProductoDto>> GetStockPorProductoAsync(long idEntidad);
    }
}
