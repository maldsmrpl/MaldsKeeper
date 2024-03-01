using MaldsKeeperWebApp.Data;
using MaldsKeeperWebApp.Interfaces;
using MaldsKeeperWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace MaldsKeeperWebApp.Repository
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public RecordRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public bool Create(Record record)
        {
            _context.Add(record);
            return Save();
        }
        public bool Edit(Record record)
        {
            _context.Update(record);
            return Save();
        }
        public bool Delete(Record record)
        {
            _context.Remove(record);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
        public async Task<IEnumerable<Record>> GetRecordsByUserIdAsync(string appUserId)
        {
            return await _context.Records.Where(u => u.AppUserId.Equals(appUserId)).ToListAsync();
        }
        public async Task<IEnumerable<Card>> GetCardsByUserIdAsync(string appUserId)
        {
            return await _context.Cards.Where(u => u.AppUserId.Equals(appUserId)).ToListAsync();
        }
        public IEnumerable<Record> GetRecordsByUser(AppUser appUser)
        {
            return _userRepository.GetUserById(appUser.Id).Records.ToList();
        }
    }
}
