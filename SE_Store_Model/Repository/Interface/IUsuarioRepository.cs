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
    public interface IUsuarioRepository
    {
        Task<UsuarioDto?> GetByIdAsync(long idUsuario);
        Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerUsuarioRequest request, long idEntidad);
        Task<UsuarioDto> InsertAsync(UsuarioDto usuario);
        Task UpdateAsync(UsuarioDto usuario);
        Task DeleteAsync(long idUsuario);
        Task UpdateEstadoAsync(long idUsuario, long idEstado);
        Task<UsuarioDto> LoginAsync(LoginRequest request);
    }
}
