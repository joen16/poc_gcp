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
    public class GrupoProductoService : IGrupoProductoService
    {
        IGrupoProductoRepository _grupoProductoRepository;
        IProductoRepository _productoRepository;
        IMapper _mapper;
        ILogger<GrupoProductoService> _logger;

        public GrupoProductoService(ILogger<GrupoProductoService> logger,
             IMapper mapper,
            IGrupoProductoRepository grupoProductoRepository,
            IProductoRepository productoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _grupoProductoRepository = grupoProductoRepository;
            _productoRepository = productoRepository;
        }

        public async Task<GrupoProductoDto> GetByIdAsync(long idGrupoProducto, long idEntidad)
        {
            var dto = await _grupoProductoRepository.GetByIdAsync(idGrupoProducto, idEntidad);
            return dto;
        }

        public async Task<GrupoProductoDto> SaveAsync(CrearGrupoProductoRequest grupoProductoRequest, long idEntidad)
        {
            var grupoProductoDto = _mapper.Map<GrupoProductoDto>(grupoProductoRequest);

            grupoProductoDto.Entidad = new EntidadDto() { Id = 1 };
            if (grupoProductoDto.Id > 0)
            {
                var grupoProductoBD = await _grupoProductoRepository.GetByIdAsync(grupoProductoDto.Id, idEntidad);
                grupoProductoDto.Estado = grupoProductoBD.Estado;
                grupoProductoDto.FechaCreacion = grupoProductoBD.FechaCreacion;
            }
            else
            {
                grupoProductoDto.FechaCreacion = DateTime.Now;
                grupoProductoDto.Estado = new EstadoDto() { Id = (int)EstadoGrupoProductoEnum.ACTIVO };
            }
            foreach (var item in grupoProductoDto.ListProducto)
            {
                item.Entidad = grupoProductoDto.Entidad;
                item.Marca = new TipoDto { Id = grupoProductoDto.Marca.Id };
                item.Categoria = new TipoDto { Id = grupoProductoDto.Categoria.Id };
                item.Color = new TipoDto { Id = grupoProductoDto.Color.Id };
                item.Nombre = grupoProductoDto.Nombre;
                item.Precio = grupoProductoDto.Precio;
                item.FechaCreacion = DateTime.Now;
                item.ListProductoDocumento = new List<ProductoDocumentoDto>();
                item.ListProductoDocumento.Add(new ProductoDocumentoDto()
                {
                    Documento = new DocumentoDto() { Id = grupoProductoRequest .IdDocumento },
                    EsActivo = true
                });

                if (item.Id > 0)
                {
                    var productoBD = await _productoRepository.GetByIdAsync(item.Id);
                    item.Estado = productoBD.Estado;
                    item.FechaCreacion = productoBD.FechaCreacion;
                }
                else
                {
                    item.Estado = new EstadoDto() { Id = (int)EstadoProductoEnum.ACTIVO };
                    item.FechaCreacion = DateTime.Now;
                }
            }

            if (grupoProductoDto.Id > 0)
            {
                var dto = await _grupoProductoRepository.UpdateAsync(grupoProductoDto);
            }
            else
            {
                var dto = await _grupoProductoRepository.InsertAsync(grupoProductoDto);
                grupoProductoDto.Id = dto.Id;
            }

            return grupoProductoDto;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerGrupoProductoRequest request, long idEntidad)
        {
            var dto = await _grupoProductoRepository.GetAsync(paginado, request, idEntidad);
            return dto;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerGrupoProductFilterRequest request, long idEntidad)
        {
            var dto = await _grupoProductoRepository.GetAsync(paginado, request, idEntidad);
            return dto;
        }

        public async Task DeleteAsync(long idGrupoProducto, long idEntidad)
        {
            var gp = await _grupoProductoRepository.GetByIdAsync(idGrupoProducto, idEntidad);
            if (gp != null)
            {
                await _grupoProductoRepository.DeleteAsync(idGrupoProducto);
            }
        }

        public async Task<List<GrupoProductoDto>> GetPopularAsync(long idEntidad)
        {
            var dto = await _grupoProductoRepository.GetPopularAsync(idEntidad);
            return dto;
        }
    }
}
