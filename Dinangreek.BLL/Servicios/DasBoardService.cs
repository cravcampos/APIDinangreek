using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Dinangreek.BLL.Servicios.Contrato;
using Dinangreek.DAL.Repositorios.Contrato;
using Dinangreek.DTO;
using Dinangreek.Model;


namespace Dinangreek.BLL.Servicios
{
    public class DasBoardService: IDashBoardService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IGenericRepository<Producto> _productoRepository;


        public DasBoardService(
            IPrestamoRepository prestamoRepository, 
            IGenericRepository<Producto> productoRepository
            )
        {
            _prestamoRepository = prestamoRepository;
            _productoRepository = productoRepository;

        }

        private IQueryable<Prestamo> RetornarPrestamos(IQueryable<Prestamo> tablaPrestamos, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaPrestamos.OrderByDescending(p => p.FechaRegistro).Select(p => p.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);
            
            return tablaPrestamos.Where(v => v.FechaRegistro >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalPrestamosUltimoMes()
        {
            int total = 0;

            IQueryable<Prestamo> _prestamoQuery = await _prestamoRepository.Consultar();

            if(_prestamoQuery.Count() > 0)
            {
                var tablaPrestamo = RetornarPrestamos(_prestamoQuery, -30);
                total = tablaPrestamo.Count();
            }

            return total;

        }

        private async Task<string> TotalIngresosUltimoMes()
        {
            decimal resultado = 0;
            IQueryable<Prestamo> _prestamoQuery = await _prestamoRepository.Consultar();

            if(_prestamoQuery.Count() > 0)
            {
                var tablaPrestamo = RetornarPrestamos(_prestamoQuery, -30);

                resultado = tablaPrestamo.Select(pre => pre.Total).Sum(p => p.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-CO"));
        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> _productoQuery = await _productoRepository.Consultar();

            int total = _productoQuery.Count();

            return total;
        }

        private async Task<Dictionary<string,int>> PrestamosUltimoMes()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            IQueryable<Prestamo> _prestamoQuery = await _prestamoRepository.Consultar();

            if(_prestamoQuery.Count() > 0)
            {
                var tablaPrestamo = RetornarPrestamos(_prestamoQuery, -30);

                resultado = tablaPrestamo
                    .GroupBy(p => p.FechaRegistro.Value.Date)
                    .OrderBy(p => p.Key)
                    .Select(dp => new {fecha = dp.Key.ToString("dd/MM/yyyy"), total = dp.Count()})
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            return resultado;
        }

        public async Task<DashBoardDTO> Resumen()
        {
            DashBoardDTO vmDashBoard = new DashBoardDTO();

            try 
            {
                vmDashBoard.TotalPrestamos = await TotalPrestamosUltimoMes();
                vmDashBoard.TotalIngresos = await TotalIngresosUltimoMes();
                vmDashBoard.TotalProductos = await TotalProductos();

                List<PrestamoMesDTO> listaPrestamosMes = new List<PrestamoMesDTO>();

                foreach(KeyValuePair<string, int> item in await PrestamosUltimoMes())
                {
                    listaPrestamosMes.Add(new PrestamoMesDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                vmDashBoard.PrestamosUltimoMes = listaPrestamosMes;
            }
            catch 
            {
                throw;
            }

            return vmDashBoard;
        }
    }
}
