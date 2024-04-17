using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class UserInPost
    {
        public int id {  get; set; }
        public string username {  get; set; }

        public UserInPost(int id, string username)
        {
            this.id = id;
            this.username = username;
        }
    }
}
