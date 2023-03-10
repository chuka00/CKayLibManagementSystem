namespace Asp.NetMVCCrud.Models
{
    public class UpdateBookVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime DateBorrowed { get; set; }
        public bool Status { get; set; }
    }
}
