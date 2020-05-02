using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class BeerDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? StyleID { get; set; }
        public BeerStyleDTO Style { get; set; }
        public int? CountryID { get; set; }
        public CountryDTO Country { get; set; }
        public int? BreweryID { get; set; }
        public BreweryDTO Brewery { get; set; }
        public double Rating { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
        public float ABV { get; set; }

    }
}
