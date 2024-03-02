namespace MaldsKeeperWebApp.ViewModels
{
    public class EditRecordViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; }
    }
}
