using GestionSuperheroes.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSuperheroes.Servicios
{
    public interface IUniversoServicio
    {
        Task<List<Universo>> ObtenerTodosAsync();
    }

    public class UniversoServicio : IUniversoServicio
    {
        private readonly HeroesBdContext _context;

        public UniversoServicio(HeroesBdContext context)
        {
            _context = context;
        }

        public async Task<List<Universo>> ObtenerTodosAsync()
        {
            return await _context.Universos
                .OrderBy(u => u.NombreUniverso)
                .ToListAsync();
        }
    }
}
