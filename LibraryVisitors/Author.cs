namespace LibraryVisitors;

public class Author
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<BookToAutors> BookToAutors { get; set; }
    public Author() 
    {
        BookToAutors = new List<BookToAutors>();
    }
}