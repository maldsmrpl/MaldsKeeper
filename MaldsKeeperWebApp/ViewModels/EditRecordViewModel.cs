namespace MaldsKeeperWebApp.ViewModels
{
    public class EditRecordViewModel
    {
        public string Title { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; }
        public string AppUserId { get; set; }
        public DateTime? AddedTime { get; set; }
        public DateTime? EditedTime { get; set; }
    }
}
