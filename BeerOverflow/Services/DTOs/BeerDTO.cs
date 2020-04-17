using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class BeerDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BeerStyleDTO Style { get; set; }
        public CountryDTO Country { get; set; }
        public BreweryDTO Brewery { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
<<<<<<< HEAD
        public float ABV { get; set; }
=======
>>>>>>> 765fe8b6b0c0dbe3120b8c9cae3b7f3d48878a6f
    }
}
