using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string TokenExpired { get; set; }
    }
}
