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
    public interface IOrdenRepository
    {
        Task<OrdenDto> GetByIdAsync(long idOrden, long idEntidad);
        Task<OrdenDto> GetByNumeroOrdenAsync(long idEntidad, string numeroOrden);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerOrdenRequest request, long idEntidad);
        Task<OrdenDto?> InsertAsync(OrdenDto ordenDto);
        Task<OrdenDto?> UpdateAsync(OrdenDto ordenDto);
        Task DeleteAsync(OrdenDto ordenDto);
        Task UpdateEstadoAsync(long idOrden, long idEstado);
        Task UpdateNumeroOrdenAsync(long idOrden, string numeroOrden);
    }
}
