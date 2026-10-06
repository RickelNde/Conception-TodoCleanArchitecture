using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAll() 
    {
        return await _context.Users.ToListAsync();
    }
    public async Task<User?> FindById(Guid id) 
    {
        return await _context.Users
           .Where(x => x.Id == id)
           .SingleOrDefaultAsync();
    }
    public async Task<User> AddUser(User user) 
    {
        EntityEntry<User> newUser = await _context.Users.AddAsync(user); // appelle la méthode AddAsync
        await _context.SaveChangesAsync(); // sauvegarde les changements dans la base de données
        return newUser.Entity; // retourne l'entité ajoutée.////
    }
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}