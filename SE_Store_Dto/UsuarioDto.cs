using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class UsuarioDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public EstadoDto Estado { get; set; }
        public EntidadDto Entidad { get; set; }
        public RolDto Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public PasswordDto Password { get; set; }
    }
}
