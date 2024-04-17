using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string text { get; set; }
        public int postId { get; set; }
        public int userId { get; set; }
        public DateTime uploadDate { get; set; }
        public UserInComment user { get; set; }

        public Comment(int id, string text, int postId, int userId, DateTime uploadDate, UserInComment user)
        {
            this.id = id;
            this.text = text;
            this.postId = postId;
            this.userId = userId;
            this.uploadDate = uploadDate;
            this.user = user;
        }
    }
}
