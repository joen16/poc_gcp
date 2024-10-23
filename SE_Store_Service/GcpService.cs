using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Integration.Izipay.CreateToken;
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
    public class GcpService : IGcpService
    {
        ILogger<GcpService> _logger;
        IParametroRepository _parametroRepository;
        IConfiguration _configuration;


        public GcpService(ILogger<GcpService> logger,
            IParametroRepository parametroRepository,
            IConfiguration configuration)
        {
            _logger = logger;
            _parametroRepository = parametroRepository;
            _configuration = configuration;
        }

        public async Task<string> UploadFileAsync(FileDto file, string fileName)
        {
            var bucketParam = _configuration[ConfigEnum.STORE_BUCKET_ID];
            if (bucketParam == null)
            {
                throw new BusinessException(string.Format("Parametro no encontrado:{0}", ConfigEnum.STORE_BUCKET_ID));
            }
            var urlParam = _configuration[ConfigEnum.STORE_BUCKET_URL_PUBLIC];
            if (urlParam == null)
            {
                throw new BusinessException(string.Format("Parametro no encontrado:{0}", ConfigEnum.STORE_BUCKET_URL_PUBLIC));
            }
           

            GcpIntegration gcpIntegration = new GcpIntegration();
            gcpIntegration.InitBucket(bucketParam);

            gcpIntegration.UploadFile(fileName, file.ContentType, file.Data);

            string url = string.Format("{0}/{1}", urlParam, fileName);
            return url;
        }

    }
}
