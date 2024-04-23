﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public string Title { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public UserInPost user { get; set; }
        public List<Comment> Comments { get; set; }
        public string displayUsername { get; set; }

        public Post(int id, string description, DateTime uploadDate, string title, int like,int dislike, UserInPost userinpost, List<Comment> comments)
        {
            Id = id;
            Description = description;
            UploadDate = uploadDate;
            Title = title;
            Like = like;
            this.Dislike = dislike;
            this.user = userinpost;
            Comments = comments;
            this.displayUsername = userinpost.username;
        }
        public Post()
        {
        }
    }
}
