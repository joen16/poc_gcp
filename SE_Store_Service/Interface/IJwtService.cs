using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service.Interface
{
    public interface IJwtService
    {
        Task<string> GenerateToken(Dictionary<string, string> liste);
        Task<string> GenerateRefreshToken(Dictionary<string, string> list);
    }
}
