using System.Text;
using ticket_dashboard.Models;
using ticket_dashboard.Services.Repository;
using System.Security.Cryptography;

namespace ticket_dashboard.Services
{
    public class AuthService
    {
        private readonly Repository<User> _userRepository;

        public AuthService(Repository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User? ValidateUser(string username, string password)
        {
            string hashed = HashPassword(password);
            return _userRepository.GetAll().FirstOrDefault(u => u.Username == username && u.PasswordHash == hashed);
        }

        private string HashPassword(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
