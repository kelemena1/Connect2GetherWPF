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
        public UserInSus user { get; set; }
        public string username { get { return user.username; } set { 
            username = user.username;
            } }
        public string description { get;set; }

        public SuspiciousUser(int id, int userId, UserInSus user, string description)
        {
            this.id = id;
            this.userId = userId;
            this.user = user;
            this.description = description;
        }
    }
}
