using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Integration.Izipay.CreateToken
{
    public class CreateTokenRequest
    {
        public string amount { get; set; }
        public string merchantCode { get; set; }
        public string orderNumber { get; set; }
        public string publicKey { get; set; }
        public string requestSource { get; set; }
    }
}
