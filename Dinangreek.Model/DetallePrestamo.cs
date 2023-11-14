using System;
using System.Collections.Generic;

namespace Dinangreek.Model;

public partial class DetallePrestamo
{
    public int IdDetallePrestamo { get; set; }

    public int? IdPrestamo { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public virtual Prestamo? IdPrestamoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
