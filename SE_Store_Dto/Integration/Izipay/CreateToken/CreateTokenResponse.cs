using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Integration.Izipay.CreateToken
{
    public class CreateTokenResponse
    {
        public string code { get; set; }
        public string message { get; set; }
        public Response response { get; set; }
    }
}
