using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dinangreek.DTO;

namespace Dinangreek.BLL.Servicios.Contrato
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> ListaMenus(int idUsuario);
    }
}
