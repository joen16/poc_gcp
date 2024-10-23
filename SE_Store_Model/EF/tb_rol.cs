using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_rol
{
    public long rol_id { get; set; }

    public string rol_nombre { get; set; } = null!;

    public virtual ICollection<tb_rol_funcionalidad> tb_rol_funcionalidad { get; set; } = new List<tb_rol_funcionalidad>();

    public virtual ICollection<tb_usuario> tb_usuario { get; set; } = new List<tb_usuario>();
}
