using System.Threading.Tasks;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetByIdAsync(string userId);
        Task UpdateProfileAsync(string userId, string newUserName, string newEmail);
        Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task DeleteAccountAsync(string userId, string confirmPassword);
        string HashPassword(string rawPassword);
    }
}