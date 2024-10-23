using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_producto_documento
{
    public int pdo_id { get; set; }

    public long pro_id { get; set; }

    public long doc_id { get; set; }

    public bool pdo_es_activo { get; set; }

    public virtual tb_documento doc { get; set; } = null!;

    public virtual tb_producto pro { get; set; } = null!;
}
