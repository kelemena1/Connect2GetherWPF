using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string hash { get; set; }
        public string email { get; set; }
        public bool activeUser { get; set; }
        public int rankId { get; set; }
        public DateTime registrationDate { get; set; }
        public int point { get; set; }
        public int permissionId { get; set; }
        public DateTime lastLogin { get; set; }
        public Permission? permission { get; set; }

        public User(int id, string username, string hash, string email, bool activeUser, int rankId, DateTime registrationDate, int point, int permissionId, DateTime lastLogin, Permission permission)
        {
            this.id = id;
            this.username = username;
            this.hash = hash;
            this.email = email;
            this.activeUser = activeUser;
            this.rankId = rankId;
            this.registrationDate = registrationDate;
            this.point = point;
            this.permissionId = permissionId;
            this.lastLogin = lastLogin;
            this.permission = permission;
        }
    }
}
