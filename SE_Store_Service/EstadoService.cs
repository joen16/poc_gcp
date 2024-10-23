using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Integration.Izipay.CreateToken;
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
    public class EstadoService : IEstadoService
    {
        ILogger<EstadoService> _logger;
        IEstadoRepository _estadoRepository;

        public EstadoService(ILogger<EstadoService> logger,
            IEstadoRepository estadoRepository)
        {
            _logger = logger;
            _estadoRepository = estadoRepository;
        }

        public async Task<ResponseEntity> GetByClasificacionAsync(long idClasificacion)
        {
            ResponseEntity result = new ResponseEntity();

            var list = await _estadoRepository.GetByClasificacionAsync(idClasificacion);

            result.Code = 200;
            result.Message = "OK";
            result.Data = list;
            return result;
        }

    }
}
