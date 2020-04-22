using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int BeerID { get; set; }
        public Beer Beer { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        public string Description { get; set; }
        public int LikesCount { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFlagged { get; set; }





    }
}
