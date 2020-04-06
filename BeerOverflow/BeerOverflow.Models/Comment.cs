using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Models
{
    public class Comment
    {
        public Guid ID { get; set; }
        public Guid BeerID { get; set; }
        public Beer Beer { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public int LikesCount { get; set; }


        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date)]
        public DateTime ModifiedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
