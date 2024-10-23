using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SE_Store_Dto;
using SE_Store_Dto.Enum;
using SE_Store_Model.Repository.Interface;
using SE_Store_Service.Interface;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;


namespace SE_Store_Service
{
    public class JwtService : IJwtService
    {
        IParametroRepository _parametroRepository;
        IMapper _mapper;
        ILogger<JwtService> _logger;
        IConfiguration _configuration;

        private string _secretKey;
        private string _audience;
        private string _issuer;
        private string _expireMinutes;
        private string _expireDaysRefresh;

        public JwtService(ILogger<JwtService> logger,
            IMapper mapper,
            IParametroRepository parametroRepository,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;

            this._secretKey = _configuration[ConfigEnum.JWT_SECRET_KEY];
            this._audience = _configuration[ConfigEnum.JWT_AUDIENCE_TOKEN];
            this._issuer = _configuration[ConfigEnum.JWT_ISSUER_TOKEN];
            this._expireMinutes = _configuration[ConfigEnum.JWT_EXPIRE_MINUTES];
            this._expireDaysRefresh = _configuration[ConfigEnum.JWT_REFRESH_EXPIRE_DAYS];
        }

        private void ValidateParams()
        {
            string paramSecretKey = _configuration[ConfigEnum.JWT_SECRET_KEY];
            if (paramSecretKey == null)
            {
                throw new Exception(string.Format("Parametro [{0}] no configurado", ConfigEnum.JWT_SECRET_KEY));
            }
            string paramAudience = _configuration[ConfigEnum.JWT_AUDIENCE_TOKEN];
            if (paramAudience == null)
            {
                throw new Exception(string.Format("Parametro [{0}] no configurado", ConfigEnum.JWT_AUDIENCE_TOKEN));
            }
            string paramIssuer = _configuration[ConfigEnum.JWT_ISSUER_TOKEN];
            if (paramIssuer == null)
            {
                throw new Exception(string.Format("Parametro [{0}] no configurado", ConfigEnum.JWT_ISSUER_TOKEN));
            }
            string paramExpireMinutes = _configuration[ConfigEnum.JWT_EXPIRE_MINUTES];
            if (paramExpireMinutes == null)
            {
                throw new Exception(string.Format("Parametro [{0}] no configurado", ConfigEnum.JWT_EXPIRE_MINUTES));
            }
            string paramExpireDaysRefresh = _configuration[ConfigEnum.JWT_REFRESH_EXPIRE_DAYS];
            if (paramExpireDaysRefresh == null)
            {
                throw new Exception(string.Format("Parametro [{0}] no configurado", ConfigEnum.JWT_REFRESH_EXPIRE_DAYS));
            }
        }


        /// <summary>
        /// Permite generar token de seguridad
        /// </summary>
        /// <param name="username"></param>
        /// <param name="isRefresh"></param>
        /// <returns></returns>
       /* public async Task<string> GenerateTokenAsync(string username, bool isRefresh = false)
        {
            string token;
            if (!isRefresh)
            {
                TokenGenerator TokenGenerator = new TokenGenerator(paramSecretKey.Valor, paramAudience.Valor, paramIssuer.Valor, Convert.ToInt32(paramExpireMinutes.Valor));
                token = TokenGenerator.GenerateTokenJwt(username);
            }
            else
            {
                TokenGenerator TokenGenerator = new TokenGenerator(paramSecretKey.Valor + "-refresh", paramAudience.Valor, paramIssuer.Valor, Convert.ToInt32(paramExpireMinutes.Valor));
                token = TokenGenerator.GenerateRefreshTokenJwt(username, Convert.ToInt32(paramExpireDaysRefresh.Valor));
            }

            return token;
        }*/

        /// <summary>
        /// Permite generar token de refresco
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /*public async Task<string> GenerateRefreshTokenAsync(string username)
        {
            return await GenerateTokenAsync(username, true);
        }

        /// <summary>
        /// Permite validar token de refresco
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task ValidateRefreshTokenAsync(string token)
        {
            TokenValidator tokenValidator = new TokenValidator(paramSecretKey.Valor + "-refresh", paramAudience.Valor, paramIssuer.Valor);
            tokenValidator.Validate(token);
        }*/


        public async Task<string> GenerateToken(Dictionary<string, string> list)
        {
            //_logger.LogDebug("GenerateToken Init");
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> listClaim = new List<Claim>();
            foreach (var claim in list)
            {
                listClaim.Add(new Claim(claim.Key, claim.Value));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(listClaim);

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: _audience,
                issuer: _issuer,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt64(_expireMinutes)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            //_logger.LogDebug("GenerateToken End");
            return jwtTokenString;
        }

        public async Task<string> GenerateRefreshToken(Dictionary<string, string> list)
        {
            //_logger.LogDebug("GenerateToken Init");
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> listClaim = new List<Claim>();
            foreach (var claim in list)
            {
                listClaim.Add(new Claim(claim.Key, claim.Value));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(listClaim);

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: _audience,
                issuer: _issuer,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt64(_expireDaysRefresh)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            //_logger.LogDebug("GenerateToken End");
            return jwtTokenString;
        }

    }
}
