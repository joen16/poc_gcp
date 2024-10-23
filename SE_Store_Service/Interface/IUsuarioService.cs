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
    public interface IUsuarioService
    {
        Task<UsuarioDto> GetByIdAsync(long idUsuario);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerUsuarioRequest request, long idEntidad);
        Task<UsuarioDto> SaveAsync(CrearUsuarioRequest request, long idEntidad);
        Task DeleteAsync(long idUsuario, long idEntidad);
        Task UpdateEstadoAsync(long idUsuario, long idEstado, long idEntidad);
    }
}
