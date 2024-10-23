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
    public class ClienteService : IClienteService
    {
        IClienteRepository _clienteRepository;
        IMapper _mapper;
        ILogger<ClienteService> _logger;

        public ClienteService(ILogger<ClienteService> logger,
             IMapper mapper,
            IClienteRepository clienteRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDto> GetByCodigoAsync(string codigo, long idEntidad)
        {
            var dto = await _clienteRepository.GetByCodigoAsync(codigo, idEntidad);
            return dto;
        }
    }
}
