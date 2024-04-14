using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class Login
    {
        /* "userName": "string",
           "password": "string"*/

        public string userName {  get; set; }
        public string password { get; set; }

        public Login(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public Login()
        {
        }
    }
}
