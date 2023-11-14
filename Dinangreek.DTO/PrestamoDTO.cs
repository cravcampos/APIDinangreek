using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinangreek.DTO
{
    public class PrestamoDTO
    {
        public int IdPrestamo { get; set; }

        public string? NumeroOrden { get; set; }

        public string? TipoPago { get; set; }

        public string? TotalTexto { get; set; }

        public string? FechaRegistro { get; set; }

        public virtual ICollection<DetallePrestamoDTO>? DetallePrestamo { get; set; }

    }
}
