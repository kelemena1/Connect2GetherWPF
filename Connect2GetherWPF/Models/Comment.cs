using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    internal class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime UploadDate { get; set; }
        public UserInComment User { get; set; }

        public Comment(int id, string text, int postId, int userId, DateTime uploadDate, UserInComment user)
        {
            Id = id;
            Text = text;
            PostId = postId;
            UserId = userId;
            UploadDate = uploadDate;
            User = user;
        }
    }
}
