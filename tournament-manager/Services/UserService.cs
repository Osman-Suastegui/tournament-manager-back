using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using tournament_manager.DTOS.Users;
using tournament_manager.Models;
using tournament_manager.Repositories.Interfaces;

namespace tournament_manager.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, ApplicationDbContext context, JwtService jwtService)
        {
            _userRepository = userRepository;
            _context = context;
            _jwtService = jwtService;
        }
        public async Task AddUserAsync(Usuario user)
        {
            Console.WriteLine("save?");
            await _userRepository.AddUserAsync(user);
        }
        
        public async Task<string?> Login(LoginRequest loginReq)
        {
            string? email = loginReq.Email;
            string? password = loginReq.Password;
            Console.WriteLine("email " + email);
            Console.WriteLine("password " + password);

            var user = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user is null)
            {
                return null;

            }

            string token = _jwtService.GenerateToken(user.Id.ToString(), user.Email);

            return token;
        }
    }
}
