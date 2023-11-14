using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dinangreek.DAL.Repositorios.Contrato;
using Dinangreek.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dinangreek.DAL.Repositorios
{
    public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
    {
        private readonly DinangreekContext _dinangreekContext;

        public GenericRepository(DinangreekContext dinangreekContext)
        {
            _dinangreekContext = dinangreekContext;
        }

        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                TModelo modelo = await _dinangreekContext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _dinangreekContext.Set<TModelo>().Add(modelo);
                await _dinangreekContext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _dinangreekContext.Set<TModelo>().Update(modelo);
                await _dinangreekContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {
                _dinangreekContext.Set<TModelo>().Remove(modelo);
                await _dinangreekContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModelo> queryModelo = filtro == null ? _dinangreekContext.Set<TModelo>() :_dinangreekContext.Set<TModelo>().Where(filtro);
                return queryModelo;

            }
            catch
            {
                throw;
            }
        }

    }
}
