using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business
{
    public class Authentication : IAuthentication
    {
        private readonly ChatAppContext context = new ChatAppContext();

        public bool Authenticate(string username, string password)
        {
            var result = context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (result == null)
                return false;

            return true;
        }

        public bool Logout()
        {
            return true;
        }
    }
}
