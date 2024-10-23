using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class CrearUsuarioRequest
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public long IdRol { get; set; }

    }
}
