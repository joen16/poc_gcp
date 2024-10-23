using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Integration.Izipay.CreateToken;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using SE_Store_Helper.Exceptions;
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
    public class RolService : IRolService
    {
        IRolRepository _rolRepository;
        IMapper _mapper;
        ILogger<RolService> _logger;

        public RolService(ILogger<RolService> logger,
            IMapper mapper,
            IRolRepository rolRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rolRepository = rolRepository;
        }


        public async Task<List<RolDto>> GetallAsync()
        {
            var dto = await _rolRepository.GetAllAsync();
            return dto;
        }


    }
}
