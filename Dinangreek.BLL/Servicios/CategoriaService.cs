using System;
using System.Collections.Generic;
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
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categoria> categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>> ListaCategorias()
        {
            try
            {
                var listaCategorias = await _categoriaRepository.Consultar();
                return _mapper.Map<List<CategoriaDTO>>(listaCategorias.ToList());
            }
            catch
            {
                throw;
                    }
        }
    }
}
