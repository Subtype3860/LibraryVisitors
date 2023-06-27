namespace LibraryVisitors
{
    public class BookRepository
    {
        private readonly ContextApp _contextApp;
        public BookRepository(ContextApp contextApp)
        {
            _contextApp = contextApp;
        }

        public List<Book> LibraryBook()
        {
            var model = _contextApp.Books.ToList();
            try
            {
                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Book SelectBook(int id)
        {
            var model = _contextApp.Books.Find(id);
            try
            {
                return model!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void CreateBoook(Book book)
        {
            var model = _contextApp.Books.First(f => string.Equals(f.Name!, book.Name!, StringComparison.CurrentCultureIgnoreCase)
                                                     && f.Date == book.Date);
            if (model.Id == default)
            {
                _contextApp.Books.Add(model);
                _contextApp.SaveChanges();
            }
            else
            {
                Console.WriteLine("Книга уже занесена в базу");
            }
        }

        public void RemoveBook(int id)
        {
            var model = _contextApp.Books.Find(id);
            if (model != default)
            {
                _contextApp.Books.Remove(model);
                _contextApp.SaveChanges();
            }
            else
            {
                Console.WriteLine("Книга не найдена");
            }
        }

        public void UpdeteBook(int id, string? name, DateTime? date)
        {
            var model = _contextApp.Books.Find(id);
            if (model != default)
            {
                model.Name = name ?? model.Name;
                model.Date = date ?? model.Date;
                _contextApp.Books.Update(model);
                _contextApp.SaveChanges();
            }
        }

        public List<Book> FindBookStyleToDate(int idStyle, DateTime date)
        {
            var model = _contextApp.Books.Where(w => w.StyleId == idStyle && w.Date == date).ToList();
            try
            {
                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
