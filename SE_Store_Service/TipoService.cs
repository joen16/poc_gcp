using AutoMapper;
using Google.Apis.Logging;
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
    public class TipoService : ITipoService
    {
        ILogger<TipoService> _logger;
        IMapper _mapper;
        ITipoRepository _tipoRepository;

        public TipoService(ILogger<TipoService> logger,
            IMapper mapper,
            ITipoRepository tipoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _tipoRepository = tipoRepository;
        }

        public async Task<TipoDto> GetByIdAsync(long idTipo, long idEntidad)
        {
            var dto = await _tipoRepository.GetByIdAsync(idTipo, idEntidad);
            return dto;
        }

        public async Task<List<TipoDto>> GetByClasificacionAsync(long idClasificacion)
        {
            var list = await _tipoRepository.GetByClasificacionAsync(idClasificacion);

            return list;
        }

        public async Task<TipoDto> SaveAsync(CrearTipoRequest crearTipoRequest, long idEntidad)
        {
            var tipoDto = _mapper.Map<TipoDto>(crearTipoRequest);

            tipoDto.Entidad = new EntidadDto() { Id = idEntidad };
            if (tipoDto.Id > 0)
            {
                var tipoBD = await _tipoRepository.GetByIdAsync(tipoDto.Id, idEntidad);
                tipoDto.Entidad = tipoBD.Entidad;
                tipoDto.Clasificacion = tipoBD.Clasificacion;
                tipoDto.FechaCreacion = tipoBD.FechaCreacion;
            } else
            {
                tipoDto.EsActivo = true;
                tipoDto.FechaCreacion = DateTime.Now;
            }
           

            if (tipoDto.Id > 0)
            {
                var dto = await this._tipoRepository.UpdateAsync(tipoDto);
            }
            else
            {
                var dto = await this._tipoRepository.InsertAsync(tipoDto);
                tipoDto.Id = dto.Id;
            }

            return tipoDto;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerTipoRequest request, long idEntidad)
        {
            var dto = await this._tipoRepository.GetAsync(paginado, request, idEntidad);
            return dto;
        }

        public async Task DeleteAsync(long idTipo, long idEntidad)
        {
            var gp = await this._tipoRepository.GetByIdAsync(idTipo, idEntidad);
            if (gp != null)
            {
                await this._tipoRepository.DeleteAsync(idTipo);
            }
            else
            {
                throw new BusinessException("Tipon no encontrado o no existe");
            }
        }
    }
}
