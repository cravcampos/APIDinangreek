using Dinangreek.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinangreek.DAL.Repositorios.Contrato;
using Dinangreek.DAL.Repositorios;
using Dinangreek.Utility;
using Dinangreek.BLL.Servicios.Contrato;
using Dinangreek.BLL.Servicios;

namespace Dinangreek.IOC
{
    public static class Dependencia
    {

        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DinangreekContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPrestamoRepository, PrestamoRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IPrestamoService, PrestamoService>();
            services.AddScoped<IDashBoardService, DasBoardService>();
            services.AddScoped<IMenuService, MenuService>();
        }
    }
}
