using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDProject
{
    class Admin:User
    {
        public Admin()
        {

        }
        public Admin(string username, string password)
        {
            Random random = new Random();

            this.Id = random.Next();
            this.Username = Username;
            this.Password = password;
        }
    }
}
