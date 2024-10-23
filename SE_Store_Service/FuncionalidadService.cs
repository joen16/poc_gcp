using Google.Apis.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Integration.Izipay.CreateToken;
using SE_Store_Helper.Exceptions;
using SE_Store_Helper.File;
using SE_Store_Integration;
using SE_Store_Model.Repository;
using SE_Store_Model.Repository.Interface;
using SE_Store_Service.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service
{
    public class FuncionalidadService : IFuncionalidadService
    {
        ILogger<FuncionalidadService> _logger;
        IFuncionalidadRepository _funcionalidadRepository;

        public FuncionalidadService(ILogger<FuncionalidadService> logger,
            IFuncionalidadRepository funcionalidadRepository)
        {
            _logger = logger;
            _funcionalidadRepository = funcionalidadRepository;
        }

        public async Task<List<FuncionalidadDto>> GetByRolAsync(long idRol)
        {
            var dto = await _funcionalidadRepository.GetByRolAsync(idRol);
            return dto;
        }



    }
}
