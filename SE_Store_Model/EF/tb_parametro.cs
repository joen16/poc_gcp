using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_parametro
{
    public long par_id { get; set; }

    public string? par_codigo { get; set; }

    public string? par_descripcion { get; set; }

    public string? par_valor { get; set; }
}
