using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class BreweryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
        public string Country { get; set; }
        //public ICollection<BeerDTO> Beers { get; set; }
=======
        public CountryDTO Country { get; set; }

        public ICollection<BeerDTO> Beers { get; set; }
>>>>>>> b62eadf70ca0e67de16b4842ab17f09ad90b4f13
    }
}
