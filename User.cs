using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDProject
{
    abstract class User
    {
        private readonly int Id;

        private readonly string Username;

        private readonly string Password;

        protected String ToFileFormat()
        {
            return (string.Format("{0},{1},{2}", this.Id, this.Username, this.Password));
        }
    }
}
