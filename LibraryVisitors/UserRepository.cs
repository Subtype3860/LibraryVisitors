namespace LibraryVisitors
{
    public class UserRepository
    {
        private readonly ContextApp _contextApp;
        public UserRepository(ContextApp contextApp) 
        {
            _contextApp = contextApp;
        }

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
    }
}
