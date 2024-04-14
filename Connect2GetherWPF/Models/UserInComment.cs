using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    internal class UserInComment
    {
        public string Username { get; set; }
        public Permission Permission { get; set; }

        public UserInComment(string username, Permission permission)
        {
            Username = username;
            Permission = permission;
        }
    }
}
