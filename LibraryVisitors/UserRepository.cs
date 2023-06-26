namespace LibraryVisitors
{
    public class UserRepository
    {
        private readonly ContextApp ContextApp;
        public UserRepository(ContextApp contextApp) 
        {
            ContextApp = contextApp;
        }

        public List<User> LibraryUser()
        {
            var model = ContextApp.Users.ToList();
            try
            {
                return model;
            }
            catch (Exception e) 
            {
                System.Console.WriteLine(e);
                return null!;
            }
        }

        public User SelectUser(int id)
        {
            var model = ContextApp.Users.Find(id);
            if(model != null)
            {
                return model!;
            }
            else
            {
                var user = new User { Name = "пользователь отсутствут", Email = "Email отсутствует" };
                return user;
            }
        }

        public void CreateUser(User user)
        {
            var model = ContextApp.Users.FirstOrDefault(user);
            if(model == default) 
            {
                ContextApp.Users.Add(model!);
                ContextApp.SaveChanges();
            }
            else
            {
                Console.WriteLine("Пользователь с таким именем уже зарегестрирован");
            }
        }
        public void RemoveUser(User user) 
        {
            var model = ContextApp.Users.FirstOrDefault(user);
            if(model != null) 
            {
                ContextApp.Users.Remove(model!);
                ContextApp.SaveChanges();
            }
            else
            {
                Console.WriteLine("Подльзователь не найден");
            }
        }
        public void UpdateUser(int id, string name)
        {
            var model = ContextApp.Users.Find(id);
            if(model!=null)
            {
                model.Name = name;
                ContextApp.Users.Update(model);
                ContextApp.SaveChanges();
            }
            else
            {
                Console.WriteLine("Подльзователь не обнавлён");
            }
        }
    }
}
