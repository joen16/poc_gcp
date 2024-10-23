using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class PasswordDto
    {
        public long Id { get; set; }
        public UsuarioDto Usuario { get; set; }
        public string Valor { get; set; }
        public bool EsActivo { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
