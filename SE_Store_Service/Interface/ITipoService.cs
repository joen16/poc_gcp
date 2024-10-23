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
    public interface ITipoService
    {
        Task<TipoDto> GetByIdAsync(long idTipo, long idEntidad);
        Task<List<TipoDto>> GetByClasificacionAsync(long idClasificacion);
        Task<TipoDto> SaveAsync(CrearTipoRequest crearTipoRequest, long idEntidad);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerTipoRequest request, long idEntidad);
        Task DeleteAsync(long idTipo, long idEntidad);
    }
}
