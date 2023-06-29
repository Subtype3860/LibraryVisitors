using Microsoft.EntityFrameworkCore;

namespace LibraryVisitors
{
    public class UserRepository
    {
        private readonly ContextApp _contextApp;
        public UserRepository(ContextApp contextApp) 
        {
            _contextApp = contextApp;
        }
        /// <summary>
        /// Выбор всех пользователей
        /// </summary>
        /// <returns></returns>
        public List<User> LibraryUser()
        {
            var model = _contextApp.Users.ToList();
            try
            {
                return model;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// Выбор пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User SelectUser(int id)
        {
            var model = _contextApp.Users.Find(id);
            try
            {
                return model!;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="user"></param>
        public void CreateUser(User user)
        {
            var model = _contextApp.Users.First(f=>string.Equals(f.Name!, user.Name!, StringComparison.CurrentCultureIgnoreCase) 
                                                   && string.Equals(f.Email!, user.Email!, StringComparison.CurrentCultureIgnoreCase));
            if(model.Id == default) 
            {
                _contextApp.Users.Add(model);
                _contextApp.SaveChanges();
            }
            else
            {
                Console.WriteLine("Пользователь с таким именем уже зарегестрирован");
            }
        }
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        public void RemoveUser(int id) 
        {
            var model = _contextApp.Users.Find(id);
            if(model != default) 
            {
                _contextApp.Users.Remove(model);
                _contextApp.SaveChanges();
            }
            else
            {
                Console.WriteLine("Подльзователь не найден");
            }
        }
        /// <summary>
        /// Обновление данных о пользователе
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public void UpdateUser(int id, string? name, string? email)
        {
            var model = _contextApp.Users.Find(id);
            if(model != default)
            {
                model.Name = name ?? model.Name;
                model.Email = email ?? model.Email;
                _contextApp.Users.Update(model);
                _contextApp.SaveChanges();
            }
            else
            {
                Console.WriteLine("Подльзователь не обнавлён");
            }
        }
        /// <summary>
        /// Получать булевый флаг о том, есть ли определенная книга на руках у пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool FlagCountBookToUser(int userId)
        {
            var model = _contextApp.Users
                .Include(i => i.Books).ToList();
            return model.Count > 0;
        }
        /// <summary>
        /// Получать количество книг на руках у пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CountBookToUser(int userId)
        {
            var model = _contextApp.Users
                .Include(i => i.Books).ToList().Count();
            return model;
        }
    }
}
