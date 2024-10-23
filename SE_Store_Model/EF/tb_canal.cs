using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_canal
{
    public long can_id { get; set; }

    public string? can_nombre { get; set; }

    public virtual ICollection<tb_orden> tb_orden { get; set; } = new List<tb_orden>();
}
