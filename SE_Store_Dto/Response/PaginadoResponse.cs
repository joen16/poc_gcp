using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Response
{
    public class PaginadoResponse
    {
        public int TotalPage { get; set; }
        public dynamic Data { get; set; }
    }
}
