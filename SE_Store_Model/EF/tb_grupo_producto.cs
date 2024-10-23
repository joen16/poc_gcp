using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_grupo_producto
{
    public long grp_id { get; set; }

    public long ent_id { get; set; }

    public long tip_id_categoria { get; set; }

    public long tip_id_marca { get; set; }

    public long tip_id_color { get; set; }

    public long est_id { get; set; }

    public string? grp_nombre { get; set; }

    public decimal? grp_precio { get; set; }

    public DateTime grp_fecha_creacion { get; set; }

    public virtual tb_entidad ent { get; set; } = null!;

    public virtual tb_estado est { get; set; } = null!;

    public virtual ICollection<tb_producto> tb_producto { get; set; } = new List<tb_producto>();

    public virtual tb_tipo tip_id_categoriaNavigation { get; set; } = null!;

    public virtual tb_tipo tip_id_colorNavigation { get; set; } = null!;

    public virtual tb_tipo tip_id_marcaNavigation { get; set; } = null!;
}
