using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service.Interface
{
    public interface IOrdenService
    {
        Task<OrdenDto> GetByIdAsync(long idOrden, long idEntidad);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerOrdenRequest request, long idEntidad);
        Task<OrdenDto> SaveAsync(CrearOrdenRequest crearOrdenRequest, long idEntidad);
        Task DeleteAsync(long idOrden, long idEntidad);
        Task UpdateEstadoAsync(long idOrden, long idEstado, long idEntidad);
        Task<OrdenBorradorDto> SaveBorradorAsync(CrearOrdenBorradorRequest crearOrdenRequest, long idEntidad);

        Task<OrdenDto> GetByNumeroOrdenAsync(long idEntidad, string numeroOrden);
    }
}
