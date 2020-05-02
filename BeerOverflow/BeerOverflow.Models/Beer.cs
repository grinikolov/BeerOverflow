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

        public int? ID { get; set; }
        public string Name { get; set; }
        public float ABV { get; set; }
        public BeerStyle Style { get; set; }
        public int? StyleID { get; set; }
        public int? CountryID { get; set; }
        public Country Country { get; set; }
        public int? BreweryID { get; set; }
        public Brewery Brewery { get; set; }
        public double Rating { get; set; }
        public ICollection<DrankList> DrankLists { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }





    }
}
