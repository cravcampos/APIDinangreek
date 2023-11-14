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
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashboardService;

        public DashBoardController(IDashBoardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashBoardDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _dashboardService.Resumen();
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
