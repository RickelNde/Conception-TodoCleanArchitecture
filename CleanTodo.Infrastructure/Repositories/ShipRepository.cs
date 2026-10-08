using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace CleanTodo.Infrastructure.Repositories
{
    public class ShipRepository: IShipRepository
    {
        private readonly AppDbContext _context;

        public ShipRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Ship>> GetAll()
        {
            return await _context.Ships.ToListAsync();
        }

        public async Task<Ship> AddShip(Ship ship)
        {
            EntityEntry<Ship> newShip = await _context.Ships.AddAsync(ship); // appelle la méthode AddAsync
            await _context.SaveChangesAsync(); // sauvegarde les changements dans la base de données
            return newShip.Entity; // retourne l'entité ajoutée.
        }

        public async Task<Ship?> FindById(Guid id)
        {
            return await _context.Ships
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}
