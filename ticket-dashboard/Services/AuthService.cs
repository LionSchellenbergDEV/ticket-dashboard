using Microsoft.AspNetCore.Identity;
using ticket_dashboard.Models;
using ticket_dashboard.Repositories.Interfaces;
namespace ticket_dashboard.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserModel> _userRepository;
        private readonly IPasswordHasher<UserModel> _passwordHasher;

        public AuthService(IRepository<UserModel> userRepository, IPasswordHasher<UserModel> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserModel?> ValidateUserCredentialsAsync(string username, string password)
        {

            var user = (await _userRepository.GetAllAsync())
                                .FirstOrDefault(u => u.Username == username);

            if (user == null) { return null; }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed) { return null; }

            return user;
        }

        public string HashPassword(UserModel user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
    }
}
