using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Integration.Izipay.CreateToken;
using SE_Store_Integration;
using SE_Store_Service.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service
{
    public class PagoService : IPagoService
    {
        ILogger<PagoService> _logger;
        IMapper _mapper;

        public PagoService(ILogger<PagoService> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CreateTokenResponse> GenerateToken(string transactionId, string orderNumber, decimal monto)
        {
            ResponseEntity result = new ResponseEntity();

            CreateTokenRequest request = new CreateTokenRequest()
            {
                requestSource = "ECOMMERCE",
                amount = formatNumber(monto),
                merchantCode = "4004345",
                orderNumber = orderNumber,
                publicKey = "VErethUtraQuxas57wuMuquprADrAHAb"
            };

            IziPayIntegration _int = new IziPayIntegration();
            var data = await _int.CreateTokenAsync(request, transactionId);
            return data;

        }

        private string formatNumber(decimal value)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            Console.WriteLine(value.ToString("N", nfi));

            // Displays the same value with four decimal digits.
            nfi.NumberDecimalDigits = 2;
            nfi.NumberDecimalSeparator = ".";
            nfi.NumberGroupSeparator = "";
            Console.WriteLine(value.ToString("N", nfi));
            return value.ToString("N", nfi);
        }
    }
}
