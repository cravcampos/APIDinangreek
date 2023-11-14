using System;
using System.Collections.Generic;

namespace Dinangreek.Model;

public partial class Prestamo
{
    public int IdPrestamo { get; set; }

    public string? NumeroOrden { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetallePrestamo> DetallePrestamo { get; set; } = new List<DetallePrestamo>();
}
