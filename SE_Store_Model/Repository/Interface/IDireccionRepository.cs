using AutoMapper;
using SE_Store_Dto;
using SE_Store_Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository.Interface
{
    public interface IDireccionRepository
    {
        Task<DireccionDto> GetByIdAsync(long idDireccion);
        Task<DireccionDto?> InsertAsync(DireccionDto direccionDto);
        Task<DireccionDto?> UpdateAsync(DireccionDto direccionDto);
        Task<DireccionDto> GetByCodigoClienteAsync(string codigoCliente, long idEntidad);
    }
}
