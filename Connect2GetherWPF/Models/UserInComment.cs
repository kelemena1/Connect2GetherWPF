using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class UserInComment
    {
        public string Username { get; set; }

        public UserInComment(string username)
        {
            Username = username;
        }
    }
}
