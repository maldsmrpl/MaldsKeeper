using MaldsKeeperWebApp.Data;
using MaldsKeeperWebApp.Interfaces;
using MaldsKeeperWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MaldsKeeperWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextAccessor _httpContextAccessor;

        public UserRepository(ApplicationDbContext context, HttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool Add(AppUser user)
        {
            _context.Users.Add(user);
            return Save();
        }
        public bool Delete(AppUser user)
        {
            _context.Users.Remove(user);
            return Save();
        }
        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public AppUser GetUserByEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault();
        }
        public AppUser GetUserById(string id)
        {
            return _context.Users.Where(i => i.Id == id).FirstOrDefault();
        }
        public string GetUserIdByEmail(string email)
        {
            return _context.Users.Where(e => e.Email == email).FirstOrDefault().Id.ToString();
        }
        public string GetCurrentUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
        public AppUser GetCurrentUser()
        {
            return GetUserByEmail(GetCurrentUserEmail());
        }
    }
}
