using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class BreweryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        //public ICollection<BeerDTO> Beers { get; set; }
    }
}
