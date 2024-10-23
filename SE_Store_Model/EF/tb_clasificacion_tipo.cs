using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_clasificacion_tipo
{
    public long cti_id { get; set; }

    public string? cti_nombre { get; set; }

    public virtual ICollection<tb_tipo> tb_tipo { get; set; } = new List<tb_tipo>();
}
