using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class UploadFileRequest
    {
        public string IdTipoDocumento { get; set; }
        public string OrderNumber { get; set; }
        public decimal Monto { get; set; }

    }
}
