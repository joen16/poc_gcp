using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_cliente_direccion
{
    public int cld_id { get; set; }

    public long cli_id { get; set; }

    public long dir_id { get; set; }

    public bool cld_es_activo { get; set; }

    public virtual tb_cliente cli { get; set; } = null!;

    public virtual tb_direccion dir { get; set; } = null!;
}
