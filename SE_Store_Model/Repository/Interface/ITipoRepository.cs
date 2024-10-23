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
    public interface ITipoRepository
    {
        Task<List<TipoDto>> GetByClasificacionAsync(long idClasificacion);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerTipoRequest request, long idEntidad);
        Task<TipoDto> GetByIdAsync(long idTipo, long idEntidad);
        Task<TipoDto?> InsertAsync(TipoDto tipoDto);
        Task<TipoDto?> UpdateAsync(TipoDto tipoDto);
        Task DeleteAsync(long idTipo);
    }
}
