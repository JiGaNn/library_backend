namespace library_backend.Models
{
    public class Books
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int publisher_id { get; set; }
        public int year_of_publishing { get; set; }
        public int quantity { get; set; }
        public string genre { get; set; }
    }
}
