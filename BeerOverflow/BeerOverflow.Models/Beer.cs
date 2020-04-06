using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerOverflow.Models
{
    public class Beer
    {
        public Beer()
        {
            
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public float ABV { get; set; }
        public BeerStyle Style { get; set; }
        public Guid StyleID { get; set; }
        public Country Country { get; set; }
        public Guid CountryID { get; set; }
        public Guid BreweryID { get; set; }
        public Brewery Brewery { get; set; }
        public ICollection<DrankList> DrankLists { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Comment> Comments { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date)]
        public DateTime ModifiedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }





    }
}
