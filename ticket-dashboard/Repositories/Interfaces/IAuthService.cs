using ticket_dashboard.Models;

namespace ticket_dashboard.Repositories.Interfaces
{
    public interface IAuthService
    {
        // Die Methode MUSS diesen Namen und diese Signatur haben:
        Task<UserModel?> ValidateUserCredentialsAsync(string username, string password);
        string HashPassword(UserModel user, string password);
        // ... weitere Methoden
    }
}
