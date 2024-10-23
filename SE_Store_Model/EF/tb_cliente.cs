using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_cliente
{
    public long cli_id { get; set; }

    public long ent_id { get; set; }

    public string cli_codigo { get; set; } = null!;

    public string cli_nombre { get; set; } = null!;

    public DateTime cli_fecha_creacion { get; set; }

    public long cli_telefono { get; set; }

    public string? cli_email { get; set; }

    public virtual tb_entidad ent { get; set; } = null!;

    public virtual ICollection<tb_cliente_direccion> tb_cliente_direccion { get; set; } = new List<tb_cliente_direccion>();

    public virtual ICollection<tb_orden> tb_orden { get; set; } = new List<tb_orden>();
}
