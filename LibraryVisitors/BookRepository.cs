using Microsoft.EntityFrameworkCore;

namespace LibraryVisitors
{
    public class BookRepository
    {
        private readonly ContextApp _contextApp;
        public BookRepository(ContextApp contextApp)
        {
            _contextApp = contextApp;
        }
        /// <summary>
        /// Получение всех книг
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Выбор книги по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Создание книги
        /// </summary>
        /// <param name="book"></param>

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
        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id"></param>
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
        /// <summary>
        /// Обновление данных о книги
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="date"></param>
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
        /// <summary>
        /// Получать список книг определенного жанра и вышедших между определенными годами
        /// </summary>
        /// <param name="idStyle"></param>
        /// <param name="date"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Получать количество книг определенного автора в библиотеке
        /// </summary>
        /// <returns></returns>
        public int CountAuthor()
        {
            var model = _contextApp.Authors.Count();
            return model;
        }
        /// <summary>
        /// Получать количество книг определенного жанра в библиотеке
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CountBookStyle(int id)
        {
            var model = _contextApp.Books.Where(w => w.StyleId == id)!.Count();
            return model;
        }
        /// <summary>
        /// Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке
        /// </summary>
        /// <param name="idAuthor"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool FlagToAuthorBookName(int idAuthor, string book)
        {
            var model = _contextApp.Authors
                .Include(i => i.Books.First(f=>f.Name == book)).ToList();
            return model.Count > 0;
        }
        /// <summary>
        /// Получать булевый флаг о том, есть ли определенная книга на руках у пользователя
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool FlagBookToUser(int bookId, int userId)
        {
            var model = _contextApp.Books
                .Where(w => w.UserId == userId && w.Id == bookId).ToList();
            return model.Count > 0;
        }
        /// <summary>
        /// Получение последней вышедшей книги
        /// </summary>
        /// <returns></returns>
        public List<Book> BooksMaxDate()
        {
            var date = _contextApp.Books.Max(d => d.Date);
            var model = _contextApp.Books.Where(w => w.Date == date).ToList();
            return model;
        }
        /// <summary>
        /// Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        /// </summary>
        /// <returns></returns>
        public List<Book> BooksOrderASC() 
        {
            var model = _contextApp.Books.OrderBy(o=>o.Name).ToList();
            return model;
        }
        /// <summary>
        /// Получение списка всех книг, отсортированного в порядке убывания года их выхода.
        /// </summary>
        /// <returns></returns>
        public List<Book > BooksOrderDESC() 
        {
            var model = _contextApp.Books.OrderByDescending(o=>o.Name).ToList();
            return model;
        }
    }
    
}
