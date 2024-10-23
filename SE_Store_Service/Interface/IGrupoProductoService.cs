using SE_Store_Dto;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service.Interface
{
    public interface IGrupoProductoService
    {
        Task<GrupoProductoDto> GetByIdAsync(long idGrupoProducto, long idEntidad);
        Task<GrupoProductoDto> SaveAsync(CrearGrupoProductoRequest grupoProductoRequest, long idEntidad);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerGrupoProductoRequest request, long idEntidad);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerGrupoProductFilterRequest request, long idEntidad);
        Task DeleteAsync(long idGrupoProducto, long idEntidad);
        Task<List<GrupoProductoDto>> GetPopularAsync(long idEntidad);
    }
}
