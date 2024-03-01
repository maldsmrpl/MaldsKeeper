using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaldsKeeperWebApp.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; }
        public DateTime? AddedTime { get; set; }
        public DateTime? EditedTime { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
