using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_producto
{
    public long pro_id { get; set; }

    public long ent_id { get; set; }

    public long grp_id { get; set; }

    public long est_id { get; set; }

    public long tip_id_marca { get; set; }

    public long tip_id_color { get; set; }

    public long tip_id_talla { get; set; }

    public long tip_id_categoria { get; set; }

    public string pro_nombre { get; set; } = null!;

    public decimal pro_precio { get; set; }

    public long pro_stock { get; set; }

    public DateTime pro_fecha_creacion { get; set; }

    public virtual tb_entidad ent { get; set; } = null!;

    public virtual tb_estado est { get; set; } = null!;

    public virtual tb_grupo_producto grp { get; set; } = null!;

    public virtual ICollection<tb_orden_producto> tb_orden_producto { get; set; } = new List<tb_orden_producto>();

    public virtual ICollection<tb_producto_documento> tb_producto_documento { get; set; } = new List<tb_producto_documento>();

    public virtual tb_tipo tip_id_categoriaNavigation { get; set; } = null!;

    public virtual tb_tipo tip_id_colorNavigation { get; set; } = null!;

    public virtual tb_tipo tip_id_marcaNavigation { get; set; } = null!;

    public virtual tb_tipo tip_id_tallaNavigation { get; set; } = null!;
}
