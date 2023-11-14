using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Dinangreek.BLL.Servicios.Contrato;
using Dinangreek.DTO;
using Dinangreek.API.Utilidad;


namespace Dinangreek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [Route("ListaRoles")]
        public async Task<IActionResult> ListaRoles()
        {
            var rsp = new Response<List<RolDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _rolService.ListaRoles();
            }
            catch(Exception ex) 
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
