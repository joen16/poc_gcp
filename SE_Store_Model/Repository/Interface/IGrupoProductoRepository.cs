using SE_Store_Dto;
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
    public interface IGrupoProductoRepository
    {
        Task<GrupoProductoDto> GetByIdAsync(long idProducto, long idEntidad);
        Task<GrupoProductoDto?> InsertAsync(GrupoProductoDto grupoProducto);
        Task<GrupoProductoDto?> UpdateAsync(GrupoProductoDto grupoProducto);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerGrupoProductoRequest request, long idEntidad);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerGrupoProductFilterRequest request, long idEntidad);
        Task DeleteAsync(long idGrupoProducto);
        Task<List<GrupoProductoDto>> GetPopularAsync(long idEntidad);

    }
}
