using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaldsKeeperWebApp.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string BankName { get; set; }
        public string CardNumber { get; set; }
        public string? ExpireDate { get; set; }
        public string? CvvCode { get; set; }
        public string? AccountNumber { get; set; }
        public string? SwiftCode { get; set; }
        public DateTime? AddedTime { get; set; }
        public DateTime? EditedTime { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
