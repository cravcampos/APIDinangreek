using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinangreek.DTO
{
    public class DashBoardDTO
    {
        public int TotalPrestamos { get; set; }
        public string? TotalIngresos {  get; set; }

        public int TotalProductos { get; set; }

        public List<PrestamoMesDTO> PrestamosUltimoMes { get; set; }
    }
}
