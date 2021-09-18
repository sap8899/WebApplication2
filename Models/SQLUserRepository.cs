using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly WebApplication1Context context;

        public SQLUserRepository(WebApplication1Context context)
        {
            this.context = context;
        }

        public User Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public User Delete(int UserId)
        {
            User user = context.Users.Find(UserId);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users;
        }

        public User GetUser(int UserId)
        {
            return context.Users.Find(UserId);
        }

        public User Update(User userChanges)
        {
            var user = context.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return userChanges;
        }
    }
}
