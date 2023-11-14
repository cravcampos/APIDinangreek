using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dinangreek.DTO;

namespace Dinangreek.BLL.Servicios.Contrato
{
    public interface IPrestamoService
    {
        Task<PrestamoDTO> RegistrarPrestamo(PrestamoDTO modelo);

        Task<List<PrestamoDTO>> Historial(string buscarPor, string numeroOrden, string fechaInicio, string fechaFin);

        Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin);
    }
}
