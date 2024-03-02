using MaldsKeeperWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaldsKeeperWebApp.ViewModels
{
    public class IndexRecordViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; }
        public DateTime? AddedTime { get; set; }
        public DateTime? EditedTime { get; set; }
    }
}
