using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class SuspiciousUser
    {
        public int id {  get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public string username { get; set; }

        public SuspiciousUser(int id, int userId, User user)
        {
            this.id = id;
            this.userId = userId;
            this.user = user;
            username = user.username;
        }
    }
}
