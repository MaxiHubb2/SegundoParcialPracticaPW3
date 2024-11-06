using GestionSuperheroes.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace GestionSuperheroes.Servicios
{
    public interface ISuperHeroeServicio
    {
        Task<List<Superheroe>> ObtenerTodosAsync();
        Task<Superheroe> AgregarSuperheroe(Superheroe superheroe);
        Task<List<Superheroe>> ObtenerPorUniversoAsync(int idUniverso);

        Task<Superheroe> EliminarHeroeAsync(int idSuperheroe);
    }

    public class SuperHeroeServicio : ISuperHeroeServicio
    {
        private readonly HeroesBdContext _context;

        public SuperHeroeServicio(HeroesBdContext context)
        {
            _context = context;
        }

        public async Task<Superheroe> AgregarSuperheroe(Superheroe superheroe)
        {
            superheroe.Eliminado = false;
            _context.Superheroes.Add(superheroe);
            await _context.SaveChangesAsync();

            return superheroe;
        }

        public async Task<List<Superheroe>> ObtenerTodosAsync()
        {
            return await _context.Superheroes
                .Include(s => s.IdUniversoNavigation)
                .Where(s => !s.Eliminado)
                .OrderBy(s => s.NombreSuperheroe)
                .ToListAsync();
        }

        public async Task<List<Superheroe>> ObtenerPorUniversoAsync(int idUniverso)
        {
            return await _context.Superheroes
                .Where(h => h.IdUniverso == idUniverso && !h.Eliminado) 
                .Include(h => h.IdUniversoNavigation)
                .ToListAsync();
        }

        public async Task<Superheroe> EliminarHeroeAsync(int idSuperheroe)
        {
            var superheroe = await _context.Superheroes.FindAsync(idSuperheroe);

            if (superheroe == null)
            {
                return null;
            }

            superheroe.Eliminado = true;
            await _context.SaveChangesAsync();

            return superheroe;
        }
    }
}
