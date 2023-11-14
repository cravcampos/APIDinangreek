using System;
using System.Collections.Generic;

namespace Dinangreek.Model;

public partial class NumeroOrden
{
    public int IdNumeroOrden { get; set; }

    public int UltimaOrden { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
