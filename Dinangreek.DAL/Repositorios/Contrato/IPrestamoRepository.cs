using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dinangreek.Model;

namespace Dinangreek.DAL.Repositorios.Contrato
{
    public interface IPrestamoRepository : IGenericRepository<Prestamo>
    {
        Task<Prestamo> Registrar(Prestamo modelo);

    }
}
