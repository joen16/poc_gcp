using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_modulo
{
    public long mod_id { get; set; }

    public string mod_nombre { get; set; } = null!;

    public long? mod_orden { get; set; }

    public virtual ICollection<tb_funcionalidad> tb_funcionalidad { get; set; } = new List<tb_funcionalidad>();
}
