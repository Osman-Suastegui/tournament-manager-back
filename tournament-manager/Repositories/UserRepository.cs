using tournament_manager.Models;
using tournament_manager.Repositories.Interfaces;

namespace tournament_manager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TournamentDbContext _context;

        public UserRepository(TournamentDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(Usuario user)
        {
            Console.Write("tes2t");
            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();   // 🔹 Persists changes in the database

        }

        public Task<List<Usuario>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
