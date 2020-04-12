using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Models
{
    public class Flag
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int ReviewID { get; set; }
        public Review Review { get; set; }
    }
}
