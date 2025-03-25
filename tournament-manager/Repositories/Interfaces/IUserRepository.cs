using tournament_manager.Models;

namespace tournament_manager.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Usuario>> GetAllUsersAsync();
        Task<Usuario?> GetUserByIdAsync(int id);
        Task AddUserAsync(Usuario user);
    }
}
