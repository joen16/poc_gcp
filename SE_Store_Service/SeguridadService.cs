using AutoMapper;
using Microsoft.Extensions.Logging;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Request;
using SE_Store_Helper.Encrypt;
using SE_Store_Model.Repository.Interface;
using SE_Store_Service.Interface;
using SE_Store_Helper.Extends;
using SE_Store_Helper.Exceptions;
using SE_Store_Helper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Microsoft.Extensions.Configuration;
using SE_Store_Dto.Enum;
using System.Text;

namespace SE_Store_Service
{

    public class SeguridadService : ISeguridadService
    {
        ILogger<SeguridadService> _logger;
        IMapper _mapper;
        IUsuarioRepository _usuarioRepository;
        IJwtService _jwtService;
        IConfiguration _configuration;
        

        public SeguridadService(ILogger<SeguridadService> logger,
            IMapper mapper,
            IUsuarioRepository usuarioRepository,
            IJwtService jwtService,
            IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<UserProfile> LoginAsync(LoginRequest request)
        {
            request.Clave = SecurityH256.Encrypt(request.Clave);
            UsuarioDto dto = await _usuarioRepository.LoginAsync(request);
            UserProfile userProfile = null;
            if (dto != null)
            {
                userProfile = _mapper.Map<UserProfile>(dto);

                Dictionary<string, string> list = new Dictionary<string, string>();
                list.Add("Email", dto.Email);
                list.Add("Id", dto.Id.ToString());
                list.Add("IdEntidad", dto.Entidad.Id.ToString());
                list.Add("Funcionalidades", Helper.ConvertToJson(dto.Rol.ListRolFuncionalidad.Select(x=> x.Funcionalidad.Id).ToArray()));


                userProfile.Token = await _jwtService.GenerateToken(list);
                userProfile.TokenRefresh = await _jwtService.GenerateRefreshToken(list);
            } else
            {
                throw new BusinessException("Usuario y/o clave incorrecto");
            }
            return userProfile;
        }


        public async Task<UserProfile> RefreshTokenAsync(RefreshTokenRequest request)
        {
            UserProfile userProfile = null;
            bool esTokenValida = EsTokenValido(request.RefreshToken);
            if (esTokenValida)
            {
                var claims = this.getIdFromToken(request.RefreshToken);
                string idUsuario = "0";
                string idEntidad = "0";
                if (claims.TryGetValue("Id", out idUsuario) && claims.TryGetValue("IdEntidad", out idEntidad))
                {
                    var idUsu = Convert.ToInt64(idUsuario);
                    var idEnt = Convert.ToInt64(idEntidad);
                    UsuarioDto dto = await _usuarioRepository.GetByIdAsync(idUsu);
                    
                    if (dto != null && dto.Entidad.Id == idEnt)
                    {
                        userProfile = _mapper.Map<UserProfile>(dto);

                        Dictionary<string, string> list = new Dictionary<string, string>();
                        list.Add("Email", dto.Email);
                        list.Add("Id", dto.Id.ToString());
                        list.Add("IdEntidad", dto.Entidad.Id.ToString());
                        list.Add("Funcionalidades", Helper.ConvertToJson(dto.Rol.ListRolFuncionalidad.Select(x => x.Funcionalidad.Id).ToArray()));


                        userProfile.Token = await _jwtService.GenerateToken(list);
                        userProfile.TokenRefresh = await _jwtService.GenerateRefreshToken(list);
                    }
                    else
                    {
                        throw new BusinessException("Usuario no encontrado o no existe");
                    }
                }
                else
                {
                    throw new BusinessException("Token invalido");
                }
            }
            else
            {
                throw new BusinessException("Token invalido");
            }
            return userProfile;
        }

        private bool EsTokenValido(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration[ConfigEnum.JWT_ISSUER_TOKEN],
                ValidateAudience = true,
                ValidAudience = _configuration[ConfigEnum.JWT_AUDIENCE_TOKEN],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[ConfigEnum.JWT_SECRET_KEY])),
                ValidateLifetime = true
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwt = (JwtSecurityToken)validatedToken;

                return true;
            }
            catch (SecurityTokenValidationException ex)
            {
                return false;
            }
        }

        private Dictionary<string, string> getIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var claims = new Dictionary<string, string>();

            foreach (var item in tokenS.Claims)
            {
                claims.Add(item.Type, item.Value);
            }

            return claims;
        }
    }
}

