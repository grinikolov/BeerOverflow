using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.Models
{
    public class CountryViewModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public ICollection<string> Breweries { get; set; }
        public bool IsDeleted { get; set; }
    }
}
