using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDProject
{
    sealed class Member:User
    {
        public readonly int Id;

        public readonly string Username;

        public readonly string Password;

        public Member(int id,string username, string password)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
        }
}
}
