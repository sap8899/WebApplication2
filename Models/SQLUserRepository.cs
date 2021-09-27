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
            context.TestUsers.Add(user);
            context.SaveChanges();
            return user;
        }

        public User Delete(int UserId)
        {
            User user = context.TestUsers.Find(UserId);
            if (user != null)
            {
                context.TestUsers.Remove(user);
                context.SaveChanges();
            }
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.TestUsers;
        }

        public User GetUser(int UserId)
        {
            return context.TestUsers.Find(UserId);
        }

        public User Update(User userChanges)
        {
            var user = context.TestUsers.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return userChanges;
        }
    }
}
