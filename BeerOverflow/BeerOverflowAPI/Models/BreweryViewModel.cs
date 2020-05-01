using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.Models
{
    public class BreweryViewModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? CountryID { get; set; }
        public string Country { get; set; }
        public ICollection<string> Beers { get; set; }
    }
}
