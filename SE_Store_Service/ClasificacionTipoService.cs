using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Integration.Izipay.CreateToken;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
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
    public class ClasificacionTipoService : IClasificacionTipoService
    {
        IClasificacionTipoRepository _clasificacionTipoRepository;
        IMapper _mapper;
        ILogger<ClasificacionTipoService> _logger;

        public ClasificacionTipoService(ILogger<ClasificacionTipoService> logger,
            IMapper mapper,
            IClasificacionTipoRepository clasificacionTipoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _clasificacionTipoRepository = clasificacionTipoRepository;
        }

        public async Task<List<ClasificacionTipoDto>> GetAllAsync()
        {
            var dto = await _clasificacionTipoRepository.GetAllAsync();
            return dto;
        }
    }
}
