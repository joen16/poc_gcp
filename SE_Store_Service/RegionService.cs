using AutoMapper;
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
    public class RegionService : IRegionService
    {
        ILogger<RegionService> _logger;
        IMapper _mapper;
        IRegionRepository _regionRepository;

        public RegionService(
            ILogger<RegionService> logger,
            IMapper mapper,
            IRegionRepository regionRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _regionRepository = regionRepository;
        }

        public async Task<ResponseEntity> GetAllAsync()
        {
            ResponseEntity result = new ResponseEntity();

            var listRegion = await _regionRepository.GetAllAsync();

            result.Code = 200;
            result.Message = "OK";
            result.Data = listRegion;
            return result;
        }

        public async Task<ResponseEntity> GetAllWithRelationsAsync()
        {
            ResponseEntity result = new ResponseEntity();

            var listRegion = await _regionRepository.GetAllWithRelationsAsync();

            result.Code = 200;
            result.Message = "OK";
            result.Data = listRegion;
            return result;
        }
    }
}
