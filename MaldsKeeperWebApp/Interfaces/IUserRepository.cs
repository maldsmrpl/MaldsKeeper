using MaldsKeeperWebApp.Models;

namespace MaldsKeeperWebApp.Interfaces
{
    public interface IUserRepository
    {
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
        Task<IEnumerable<AppUser>> GetAllUsers();
        AppUser GetUserByEmail(string email);
        AppUser GetUserById(string id);
        string GetUserIdByEmail(string email);
        string GetCurrentUserEmail();
        AppUser GetCurrentUser();
    }
}
