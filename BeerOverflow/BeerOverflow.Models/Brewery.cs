using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerOverflow.Models
{
    public class Brewery
    {
        public Brewery()
        {

        }
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? CountryID { get; set; }
        public Country Country { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Beer> Beers { get; set; }
    }
}