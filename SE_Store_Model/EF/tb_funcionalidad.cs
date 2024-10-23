using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_funcionalidad
{
    public long fun_id { get; set; }

    public string fun_nombre { get; set; } = null!;

    public string? fun_path { get; set; }

    public string? fun_orden { get; set; }

    public long? mod_id { get; set; }

    public string? fun_icon { get; set; }

    public virtual tb_modulo? mod { get; set; }

    public virtual ICollection<tb_rol_funcionalidad> tb_rol_funcionalidad { get; set; } = new List<tb_rol_funcionalidad>();
}
