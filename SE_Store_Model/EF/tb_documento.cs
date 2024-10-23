using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_documento
{
    public long doc_id { get; set; }

    public long tdo_id { get; set; }

    public string doc_nombre { get; set; } = null!;

    public string doc_path { get; set; } = null!;

    public long doc_peso { get; set; }

    public DateTime doc_fecha_creacion { get; set; }

    public virtual ICollection<tb_producto_documento> tb_producto_documento { get; set; } = new List<tb_producto_documento>();

    public virtual tb_tipo_documento tdo { get; set; } = null!;
}
