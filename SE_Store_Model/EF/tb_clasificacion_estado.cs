using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_clasificacion_estado
{
    public long ces_id { get; set; }

    public string? ces_nombre { get; set; }

    public virtual ICollection<tb_estado> tb_estado { get; set; } = new List<tb_estado>();
}
