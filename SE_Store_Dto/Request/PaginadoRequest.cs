using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class PaginadoRequest
    {
        public int currentPage { get; set; }
        public int regPerPage { get; set; }
    }
}
