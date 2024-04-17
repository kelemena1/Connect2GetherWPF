using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    internal class Post
    {
        public int Id { get; set; }
        public int? ImageId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Like { get; set; }
        public int UserId { get; set; }
        public DateTime UploadDate { get; set; }
        public List<Comment> Comments { get; set; }


        public Post(int id, int? imageId, string description, string title, int like, int userId, DateTime uploadDate, List<Comment> comments)
        {
            Id = id;
            ImageId = imageId;
            Description = description;
            Title = title;
            Like = like;
            UserId = userId;
            UploadDate = uploadDate;
            Comments = comments;
        }
    }
}
