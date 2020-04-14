using BeerOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class CountryDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public ICollection<BreweryDTO>? Breweries { get; set; }

    }
}
