using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFUserRepository
    {
        public IEnumerable<User> GetAllUsers()
        {
            var context = new EFDbContext();

            return context.Users.ToList();
        }

        public void SaveUser(User user)
        {
            var context = new EFDbContext();

            if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.UserName = user.UserName;
                    dbEntry.UserMail = user.UserMail;
                    dbEntry.UserPassword = user.UserPassword;
                    dbEntry.IsAdmin = false;
                }
            }
            context.SaveChanges();
        }
    }
}
