using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace MaldsKeeperWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public string Key { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime? EditedTime { get; set; }
        public ICollection<Record>? Records { get; set; }
        public ICollection<Card>? Cards { get; set; }
    }
}