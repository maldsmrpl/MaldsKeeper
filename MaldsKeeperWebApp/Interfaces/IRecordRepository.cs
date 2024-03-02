using MaldsKeeperWebApp.Models;

namespace MaldsKeeperWebApp.Interfaces
{
    public interface IRecordRepository
    {
        public Task<IEnumerable<Record>> GetRecordsByUserIdAsync(string appUserId);
        public Task<Record> GetRecordByIdAsync(int recordId);
        public Record GetRecordById(int recordId);
        public Task<IEnumerable<Card>> GetCardsByUserIdAsync(string appUserId);
        public bool Create(Record record);
        public bool Edit(Record record);
        public bool Delete(Record record);
        public bool Save();
    }
}
