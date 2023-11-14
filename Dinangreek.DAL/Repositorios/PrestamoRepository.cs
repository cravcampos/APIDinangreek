using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dinangreek.DAL.DBContext;
using Dinangreek.DAL.Repositorios.Contrato;
using Dinangreek.Model;

namespace Dinangreek.DAL.Repositorios
{
    public class PrestamoRepository : GenericRepository<Prestamo>, IPrestamoRepository
    {
        private readonly DinangreekContext _dinangreekContext;

        public PrestamoRepository(DinangreekContext dinangreekContext) : base(dinangreekContext)
        {
            _dinangreekContext = dinangreekContext;
        }

        public async Task<Prestamo> Registrar(Prestamo modelo)
        {
            Prestamo prestamoRealizado = new Prestamo();

            using (var transaction = _dinangreekContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetallePrestamo dp in modelo.DetallePrestamo)
                    {
                        Producto producto_encontrado = _dinangreekContext.Productos.Where(p => 
                        p.IdProducto == dp.IdProducto).First();
                        producto_encontrado.Stock -= dp.Cantidad;
                        _dinangreekContext.Productos.Update(producto_encontrado);
                    }
                    await _dinangreekContext.SaveChangesAsync();

                    NumeroOrden correlativo = _dinangreekContext.NumeroOrden.First();
                    correlativo.UltimaOrden ++;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dinangreekContext.NumeroOrden.Update(correlativo);
                    await _dinangreekContext.SaveChangesAsync();

                    // Generar formato de numero de orden
                    int cantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos));
                    string numeroPrestamo = ceros + correlativo.UltimaOrden.ToString();

                    numeroPrestamo = numeroPrestamo.Substring(numeroPrestamo.Length - cantidadDigitos, cantidadDigitos);
                    modelo.NumeroOrden = numeroPrestamo;

                    await _dinangreekContext.AddAsync(modelo);
                    await _dinangreekContext.SaveChangesAsync();

                    prestamoRealizado = modelo;

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                return prestamoRealizado;
            }
        }
    }
}
