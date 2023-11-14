using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Dinangreek.BLL.Servicios.Contrato;
using Dinangreek.DTO;
using Dinangreek.API.Utilidad;


namespace Dinangreek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        [Route("ListaProductos")]
        public async Task<IActionResult> ListaProductos()
        {
            var rsp = new Response<List<ProductoDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _productoService.ListaProductos();
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("GuardarProducto")]
        public async Task<IActionResult> GuardarProducto([FromBody] ProductoDTO producto)
        {
            var rsp = new Response<ProductoDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _productoService.Crear(producto);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("EditarProducto")]
        public async Task<IActionResult> EditarProducto([FromBody] ProductoDTO producto)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _productoService.Editar(producto);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("EliminarProducto/{id:int}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _productoService.Eliminar(id);
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
