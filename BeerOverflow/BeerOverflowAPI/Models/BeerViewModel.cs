using BeerOverflowAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflowAPI.Models
{
    public class BeerViewModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string Country { get; set; }
        public string Brewery { get; set; }
        public double Rating { get; set; }
        public ICollection<string> Reviews { get; set; }
        public float ABV { get; set; }

    }
}
