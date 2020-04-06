using System;
using System.Collections.Generic;

namespace BeerOverflow.Models
{
    public class User
    {
        public Guid UserID { get; set; }

        public string Name { get; set; }

        public ICollection<DrankList> DrankLists { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public bool IsDeleted { get; set; }

    }
}