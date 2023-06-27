namespace LibraryVisitors
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }
        public int StyleId { get; set; }
        public Style? Style { get; set; }
        public List<BookToAutors> BookToAutors { get; set; }
        public Book()
        {
            BookToAutors = new List<BookToAutors>();
        }
    }
}
