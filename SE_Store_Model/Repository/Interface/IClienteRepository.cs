using SE_Store_Dto;
using SE_Store_Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository.Interface
{
    public interface IClienteRepository
    {
        Task<ClienteDto?> InsertAsync(ClienteDto clienteDto);
        Task<ClienteDto?> UpdateAsync(ClienteDto clienteDto);
        Task<ClienteDto> GetByIdAsync(long idCliente, long idEntidad);
        Task<ClienteDto> GetByCodigoAsync(string codigo, long idEntidad);
    }
}
