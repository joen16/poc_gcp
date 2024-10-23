using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Integration.Izipay.CreateToken;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using SE_Store_Helper.Encrypt;
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

    public class UsuarioService : IUsuarioService
    {
        ILogger<UsuarioService> _logger;
        IMapper _mapper;
        IUsuarioRepository _usuarioRepository;

        public UsuarioService(ILogger<UsuarioService> logger,
             IMapper mapper,
            IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioDto> GetByIdAsync(long idUsuario)
        {
            var dto = await _usuarioRepository.GetByIdAsync(idUsuario);
            return dto;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerUsuarioRequest request, long idEntidad)
        {
            var dto = await _usuarioRepository.GetAsync(paginado, request, idEntidad);
            return dto;
        }

        public async Task<UsuarioDto> SaveAsync(CrearUsuarioRequest request, long idEntidad)
        {
            var usuarioDto = _mapper.Map<UsuarioDto>(request);

            if (usuarioDto.Id > 0)
            {
                var  usuarioBD = await _usuarioRepository.GetByIdAsync(usuarioDto.Id);
                if(usuarioBD != null && usuarioBD.Entidad.Id == idEntidad)
                {
                    usuarioDto.FechaCreacion = usuarioBD.FechaCreacion;
                    usuarioDto.Entidad = usuarioBD.Entidad;
                    usuarioDto.Estado = usuarioBD.Estado;
                    await _usuarioRepository.UpdateAsync(usuarioDto);
                }else
                {
                    throw new BusinessException("Usuario no encontrado o no existe");
                }
               
            } else
            {
                usuarioDto.FechaCreacion = DateTime.Now;
                usuarioDto.Entidad = new EntidadDto() { Id = idEntidad };
                usuarioDto.Estado = new EstadoDto() { Id = (long)EstadoUsuarioEnum.ACTIVO };
                usuarioDto.Password = new PasswordDto()
                {
                    EsActivo = true,
                    FechaCreacion = DateTime.Now,
                    Valor = SecurityH256.Encrypt(usuarioDto.Email),
                };
                var dto = await _usuarioRepository.InsertAsync(usuarioDto);
            }            
            return usuarioDto;
        }

        public async Task DeleteAsync(long idUsuario, long idEntidad)
        {
            var usu = await _usuarioRepository.GetByIdAsync(idUsuario);
            if (usu != null && usu.Entidad.Id == idEntidad)
            {
                await _usuarioRepository.DeleteAsync(idUsuario);
            }
            else
            {
                throw new BusinessException("Usuario no encontrado o no existe");
            }
        }

        public async Task UpdateEstadoAsync(long idUsuario, long idEstado, long idEntidad)
        {
            var usu = await _usuarioRepository.GetByIdAsync(idUsuario);
            if (usu != null && usu.Entidad.Id == idEntidad)
            {
                await _usuarioRepository.UpdateEstadoAsync(idUsuario, idEstado);
            }
            else
            {
                throw new BusinessException("Usuario no encontrado o no existe");
            }
        }

    }
}
