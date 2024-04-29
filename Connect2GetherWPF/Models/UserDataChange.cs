using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class UserDataChange
    {
        public string userName { get; set; }
        public string email { get; set; }
        public int permissionId { get; set; }

        public UserDataChange(string userName, string email, int permissionId)
        {
            this.userName = userName;
            this.email = email;
            this.permissionId = permissionId;
        }

        public UserDataChange()
        {
        }
    }
}
