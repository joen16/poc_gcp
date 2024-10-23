using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using MySqlX.XDevAPI;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
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
    public class OrdenService : IOrdenService
    {
        IOrdenRepository _ordenRepository;
        IOrdenProductoRepository _ordenProductoRepository;
        IClienteRepository _clienteRepository;
        IDireccionRepository _direccionRepository;
        IProductoRepository _productoRepository;
        IParametroRepository _parametroRepository;
        IMapper _mapper;
        ILogger<OrdenService> _logger;
        IPagoService _pagoservice;

        public OrdenService(ILogger<OrdenService> logger,
            IMapper mapper,
            IOrdenRepository ordenRepository,
            IClienteRepository clienteRepository,
            IDireccionRepository direccionRepository,
            IProductoRepository productoRepository,
            IOrdenProductoRepository ordenProductoRepository,
            IPagoService pagoService,
            IParametroRepository parametroRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _ordenRepository = ordenRepository;
            _clienteRepository = clienteRepository;
            _direccionRepository = direccionRepository;
            _productoRepository = productoRepository;
            _ordenProductoRepository = ordenProductoRepository;
            _pagoservice = pagoService;
            _parametroRepository = parametroRepository;
        }

        public async Task<OrdenDto> GetByIdAsync(long idOrden, long idEntidad)
        {
            var dto = await _ordenRepository.GetByIdAsync(idOrden, idEntidad);
            return dto;
        }

        public async Task<OrdenDto> GetByNumeroOrdenAsync(long idEntidad, string numeroOrden)
        {
            var dto = await _ordenRepository.GetByNumeroOrdenAsync(idEntidad, numeroOrden);
            return dto;
        }

        public async Task<OrdenDto> SaveAsync(CrearOrdenRequest crearOrdenRequest, long idEntidad)
        {
            var ordenDto = _mapper.Map<OrdenDto>(crearOrdenRequest);

            ordenDto.Entidad = new EntidadDto() { Id = idEntidad };
            if (ordenDto.Id > 0)
            {
                var ordenBD = await _ordenRepository.GetByIdAsync(ordenDto.Id, idEntidad);
                ordenDto.Estado = ordenBD.Estado;
                ordenDto.Entidad = ordenBD.Entidad;
                ordenDto.FechaCreacion = ordenBD.FechaCreacion;
                ordenDto.Canal = ordenBD.Canal;
            }
            else
            {
                ordenDto.FechaCreacion = DateTime.Now;

            }

            //estado
            if (ordenDto.MontoPagado == ordenDto.MontoTotal)
            {
                ordenDto.Estado = new EstadoDto() { Id = (long)EstadoOrdenEnum.CONFIRMADO };
            }
            else
            {
                ordenDto.Estado = new EstadoDto() { Id = (long)EstadoOrdenEnum.RESERVADO };
            }


            if (ordenDto.Cliente.Id > 0)
            {
                var clienteBD = await this._clienteRepository.GetByIdAsync(ordenDto.Cliente.Id, idEntidad);
                if (clienteBD != null)
                {
                    ordenDto.Cliente = clienteBD;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ordenDto.Cliente.Codigo))
                {
                    var clienteBD = await this._clienteRepository.GetByCodigoAsync(ordenDto.Cliente.Codigo, idEntidad);
                    if (clienteBD != null)
                    {
                        clienteBD.Nombre = ordenDto.Cliente.Nombre;
                        ordenDto.Cliente = clienteBD;

                    }
                    else
                    {
                        ordenDto.Cliente.FechaCreacion = DateTime.Now;
                        ordenDto.Cliente.Entidad = new EntidadDto() { Id = idEntidad };
                    }
                }
                else
                {
                    ordenDto.Cliente.FechaCreacion = DateTime.Now;
                    ordenDto.Cliente.Entidad = new EntidadDto() { Id = idEntidad };
                }
            }

            if (ordenDto.Direccion.Id > 0)
            {
                var direccionBD = await this._direccionRepository.GetByIdAsync(ordenDto.Direccion.Id);
                if (direccionBD != null)
                {
                    if (direccionBD.Region.Id == ordenDto.Direccion.Region.Id
                        && direccionBD.Provincia.Id == ordenDto.Direccion.Provincia.Id
                        && direccionBD.Distrito.Id == ordenDto.Direccion.Distrito.Id
                        && direccionBD.Agencia.Id == ordenDto.Direccion.Agencia.Id)
                    {
                        ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                        ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = direccionBD });
                        ordenDto.Direccion = direccionBD;
                    }
                    else
                    {
                        ordenDto.Direccion.Id = 0;
                        ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                        ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = ordenDto.Direccion });
                    }
                }
                else
                {
                    ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                    ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = ordenDto.Direccion });
                }
            }
            else
            {
                var ordenBD = await _ordenRepository.GetByIdAsync(ordenDto.Id, idEntidad);
                if (ordenBD != null)
                {
                    if (ordenBD.Direccion.Region.Id == ordenDto.Direccion.Region.Id
                        && ordenBD.Direccion.Provincia.Id == ordenDto.Direccion.Provincia.Id
                        && ordenBD.Direccion.Distrito.Id == ordenDto.Direccion.Distrito.Id
                        && ordenBD.Direccion.Agencia.Id == ordenDto.Direccion.Agencia.Id)
                    {
                        ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                        ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = ordenBD.Direccion });
                        ordenDto.Direccion = ordenBD.Direccion;
                    }
                    else
                    {
                        ordenDto.Direccion.Id = 0;
                        ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                        ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = ordenDto.Direccion });
                    }
                }
                else
                {
                    ordenDto.Direccion.Id = 0;
                    ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                    ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = ordenDto.Direccion });
                }
            }

            foreach (var item in ordenDto.ListOrdenProducto)
            {
                item.EsActivo = true;
                if (item.Id > 0)
                {
                    var ordenProductoBD = await _ordenProductoRepository.GetByIdAsync(item.Id);
                    var productoBD = await _productoRepository.GetByIdAsync(item.Producto.Id);
                    if (ordenProductoBD != null && productoBD != null)
                    {
                        long stock = productoBD.Stock;
                        long dif = (item.Cantidad - ordenProductoBD.Cantidad);
                        item.Producto = productoBD;
                        item.Producto.Stock = (stock - dif);
                    }
                }
                else
                {
                    item.FechaCreacion = DateTime.Now;

                    var producto = await _productoRepository.GetByIdAsync(item.Producto.Id);
                    if (producto != null)
                    {
                        long stock = producto.Stock;
                        item.Producto = producto;
                        item.Producto.Stock = (stock - item.Cantidad);
                    }
                }
            }

            if (ordenDto.Id > 0)
            {
                var dto = await _ordenRepository.UpdateAsync(ordenDto);
            }
            else
            {
                var dto = await _ordenRepository.InsertAsync(ordenDto);
                ordenDto.Id = dto.Id;
            }

            return ordenDto;
        }


        public async Task<OrdenBorradorDto> SaveBorradorAsync(CrearOrdenBorradorRequest crearOrdenRequest, long idEntidad)
        {
            var ordenDto = _mapper.Map<OrdenDto>(crearOrdenRequest);

            ordenDto.Entidad = new EntidadDto() { Id = idEntidad };
            ordenDto.Estado = new EstadoDto() { Id = (long)EstadoOrdenEnum.ESPERANDO_PAGO };
            ordenDto.FechaCreacion = DateTime.Now;


            if (!string.IsNullOrEmpty(ordenDto.Cliente.Codigo))
            {
                var clienteBD = await this._clienteRepository.GetByCodigoAsync(ordenDto.Cliente.Codigo, idEntidad);
                if (clienteBD != null)
                {
                    clienteBD.Nombre = ordenDto.Cliente.Nombre;
                    ordenDto.Cliente = clienteBD;

                }
                else
                {
                    ordenDto.Cliente.FechaCreacion = DateTime.Now;
                    ordenDto.Cliente.Entidad = new EntidadDto() { Id = idEntidad };
                }
            }
            else
            {
                throw new BusinessException("Datos de cliente no valido");
            }

            if (!string.IsNullOrEmpty(ordenDto.Cliente.Codigo))
            {
                var direccionBD = await _direccionRepository.GetByCodigoClienteAsync(ordenDto.Cliente.Codigo, idEntidad);
                if (direccionBD != null)
                {
                    if (direccionBD.Region.Id == ordenDto.Direccion.Region.Id
                        && direccionBD.Provincia.Id == ordenDto.Direccion.Provincia.Id
                        && direccionBD.Distrito.Id == ordenDto.Direccion.Distrito.Id
                        && direccionBD.Agencia.Id == ordenDto.Direccion.Agencia.Id)
                    {
                        ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                        ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = direccionBD });
                        ordenDto.Direccion = direccionBD;
                    }
                    else
                    {
                        ordenDto.Direccion.Id = 0;
                        ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                        ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = ordenDto.Direccion });
                    }
                }
                else
                {
                    ordenDto.Direccion.Id = 0;
                    ordenDto.Cliente.ListClienteDireccion = new List<ClienteDireccionDto>();
                    ordenDto.Cliente.ListClienteDireccion.Add(new ClienteDireccionDto() { Direccion = ordenDto.Direccion });
                }
            }
            else
            {
                throw new BusinessException("Datos de cliente no valido");
            }


            foreach (var item in ordenDto.ListOrdenProducto)
            {
                item.EsActivo = true;
                item.FechaCreacion = DateTime.Now;
                
                    var producto = await _productoRepository.GetByIdAsync(item.Producto.Id);
                    if (producto != null)
                    {
                    item.Producto.Stock = producto.Stock;
                        item.Producto = producto;
                    }

            }            

            var dto = await _ordenRepository.InsertAsync(ordenDto);

            var listParams = new List<string>();
            listParams.Add(ParametroEnum.IZIPAY_COMERCIO_ID);
            listParams.Add(ParametroEnum.IZIPAY_CONFIRMACION_URL);
            listParams.Add(ParametroEnum.IZIPAY_LOGO_URL);

            var parameters = await _parametroRepository.GetByListAsync(listParams);
            string idComercio = parameters.FirstOrDefault(x => x.Codigo == ParametroEnum.IZIPAY_COMERCIO_ID)?.Valor;
            string urlConfirmacion = parameters.FirstOrDefault(x => x.Codigo == ParametroEnum.IZIPAY_CONFIRMACION_URL)?.Valor;
            string urlLogo = parameters.FirstOrDefault(x => x.Codigo == ParametroEnum.IZIPAY_LOGO_URL)?.Valor;

            string numeroOrden = string.Format("ZX-IP-{0}", dto.Id.ToString());
            string idTransaction = DateTime.Now.Ticks.ToString();

            await _ordenRepository.UpdateNumeroOrdenAsync(dto.Id, numeroOrden);

            var tokenGenerator = await _pagoservice.GenerateToken(idTransaction, numeroOrden, dto.MontoTotal);

            var response = new OrdenBorradorDto()
            {
                IdOrden = numeroOrden,
                Token = tokenGenerator.response.token,
                IdComercio = idComercio,
                IdTransaccion = idTransaction,
                UrlConfirmacion = urlConfirmacion,
                UrlLogo = urlLogo,
                Monto = dto.MontoTotal
            };

            return response;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerOrdenRequest request, long idEntidad)
        {
            var dto = await _ordenRepository.GetAsync(paginado, request, idEntidad);
            return dto;
        }

        public async Task DeleteAsync(long idOrden, long idEntidad)
        {
            var ordenRepository = await _ordenRepository.GetByIdAsync(idOrden, idEntidad);
            if (ordenRepository != null)
            {
                ordenRepository.Estado.Id = (long)EstadoOrdenEnum.ELIMINADO;
                foreach (var op in ordenRepository.ListOrdenProducto)
                {
                    var productoBD = await _productoRepository.GetByIdAsync(op.Producto.Id);
                    if (productoBD != null)
                    {
                        op.Producto.Stock = (productoBD.Stock + op.Cantidad);
                    }
                }
                await _ordenRepository.DeleteAsync(ordenRepository);
            }
            else
            {
                throw new BusinessException("Orden no encontrada o no existe");
            }
        }

        public async Task UpdateEstadoAsync(long idOrden, long idEstado, long idEntidad)
        {
            var gp = await _ordenRepository.GetByIdAsync(idOrden, idEntidad);
            if (gp != null)
            {
                await _ordenRepository.UpdateEstadoAsync(idOrden, idEstado);
            }
            else
            {
                throw new BusinessException("Orden no encontrada o no existe");
            }
        }
    }
}
