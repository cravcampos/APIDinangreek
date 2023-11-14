using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Dinangreek.BLL.Servicios.Contrato;
using Dinangreek.DAL.Repositorios.Contrato;
using Dinangreek.DTO;
using Dinangreek.Model;
using Microsoft.EntityFrameworkCore;

namespace Dinangreek.BLL.Servicios
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IGenericRepository<DetallePrestamo> _detallePrestamoRepository;
        private readonly IMapper _mapper;

        public PrestamoService(
            IPrestamoRepository prestamoRepository,
            IGenericRepository<DetallePrestamo>
            detallePrestamoRepository, IMapper mapper)
        {
            _prestamoRepository = prestamoRepository;
            _detallePrestamoRepository = detallePrestamoRepository;
            _mapper = mapper;
        }

        public async Task<PrestamoDTO> RegistrarPrestamo(PrestamoDTO modelo)
        {
            try
            {
                var prestamoGenerado = await _prestamoRepository.Registrar(_mapper.Map<Prestamo>(modelo));

                if (prestamoGenerado.IdPrestamo == 0)
                    throw new TaskCanceledException("No se pudo generar el prestamo");

                return _mapper.Map<PrestamoDTO>(prestamoGenerado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PrestamoDTO>> Historial(string buscarPor, string numeroOrden, string fechaInicio, string fechaFin)
        {
            IQueryable<Prestamo> query = await _prestamoRepository.Consultar();
            var listaResultado = new List<Prestamo>();

            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                    listaResultado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                    v.FechaRegistro.Value.Date <= fecha_fin.Date
                    ).Include(dp => dp.DetallePrestamo)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }
                else
                {
                    listaResultado = await query.Where(v => v.NumeroOrden == numeroOrden)
                    .Include(dp => dp.DetallePrestamo)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<PrestamoDTO>>(listaResultado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetallePrestamo> query = await _detallePrestamoRepository.Consultar();
            var listaResultado = new List<DetallePrestamo>();

            try
            {
                DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                listaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(pre => pre.IdPrestamoNavigation)
                    .Where(dp => 
                    dp.IdPrestamoNavigation.FechaRegistro.Value.Date >=fecha_inicio.Date &&
                    dp.IdPrestamoNavigation.FechaRegistro.Value.Date <=fecha_fin.Date
                    ).ToListAsync();
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReporteDTO>>(listaResultado);
        }
    }
}
