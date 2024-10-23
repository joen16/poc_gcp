using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_rol_funcionalidad
{
    public long rfu_id { get; set; }

    public long rol_id { get; set; }

    public long fun_id { get; set; }

    public DateTime rfu_fecha_creacion { get; set; }

    public virtual tb_funcionalidad fun { get; set; } = null!;

    public virtual tb_rol rol { get; set; } = null!;
}
