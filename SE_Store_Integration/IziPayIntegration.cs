using SE_Store_Dto.Integration.Izipay.CreateToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Integration
{
    public class IziPayIntegration : BaseInt
    {
        public async Task<CreateTokenResponse> CreateTokenAsync(CreateTokenRequest request, string transactionId)
        {
            string url = "https://sandbox-checkout.izipay.pe/apidemo/v1/Token/Generate";

            Dictionary<string, Object> headers = new Dictionary<string, object>();
            headers.Add("transactionId", transactionId);
            return await this.PostAsync<CreateTokenRequest, CreateTokenResponse>(url, request, headers);
        }
    }
}
