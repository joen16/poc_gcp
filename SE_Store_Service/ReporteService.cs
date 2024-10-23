using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
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
    public class ReporteService : IReporteService
    {
        ILogger<ReporteService> _logger;
        IMapper _mapper;
        IReporteRepository _reporteRepository;

        public ReporteService(
            ILogger<ReporteService> logger,
            IMapper mapper,
            IReporteRepository reporteRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _reporteRepository = reporteRepository;
        }

        public async Task<List<ReporteStockCategoriaDto>> GetStockPorCategoriaAsync(long idEntidad)
        {
            return await _reporteRepository.GetStockPorCategoriaAsync(idEntidad);
        }

        public async Task<List<ReporteStockProductoDto>> GetStockPorProductoAsync(long idEntidad)
        {
            return await _reporteRepository.GetStockPorProductoAsync(idEntidad);
        }
    }
}
