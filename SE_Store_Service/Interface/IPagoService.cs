using SE_Store_Dto;
using SE_Store_Dto.Integration.Izipay.CreateToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service.Interface
{
    public interface IPagoService
    {
        //Task<ResponseEntity> GenerateToken(string transactionId, string orderNumber, decimal monto);
        Task<CreateTokenResponse> GenerateToken(string transactionId, string orderNumber, decimal monto);
    }
}
