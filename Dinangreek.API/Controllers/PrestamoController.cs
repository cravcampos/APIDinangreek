using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Dinangreek.BLL.Servicios.Contrato;
using Dinangreek.DTO;
using Dinangreek.API.Utilidad;
using Dinangreek.BLL.Servicios;

namespace Dinangreek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }



        [HttpPost]
        [Route("GuardarPrestamo")]
        public async Task<IActionResult> GuardarPrestamo([FromBody] PrestamoDTO prestamo)
        {
            var rsp = new Response<PrestamoDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _prestamoService.RegistrarPrestamo(prestamo);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(
            string buscarPor,
            string? numeroOrden,
            string? fechaInicio,
            string? fechaFin
        )
        {
            var rsp = new Response<List<PrestamoDTO>>();
            numeroOrden = numeroOrden is null ? "" : numeroOrden;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                rsp.Status = true;
                rsp.Value = await _prestamoService.Historial(buscarPor,numeroOrden,fechaInicio,fechaFin);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<ReporteDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _prestamoService.Reporte(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
