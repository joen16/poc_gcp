using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_orden_producto
{
    public long opr_id { get; set; }

    public long pro_id { get; set; }

    public long ord_id { get; set; }

    public long opr_cantidad { get; set; }

    public long opr_precio { get; set; }

    public decimal opr_total { get; set; }

    public bool opr_es_activo { get; set; }

    public DateTime opr_fecha_creacion { get; set; }

    public virtual tb_orden ord { get; set; } = null!;

    public virtual tb_producto pro { get; set; } = null!;
}
