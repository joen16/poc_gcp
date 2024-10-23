using SE_Store_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service.Interface
{
    public interface IFuncionalidadService
    {
        Task<List<FuncionalidadDto>> GetByRolAsync(long idRol);
    }
}
