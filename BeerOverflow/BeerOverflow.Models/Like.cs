using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Models
{
    public class Like
    {
        public int CommentID { get; set; }
        public Comment Comment { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
