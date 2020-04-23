using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Models
{
    public class BeerUserRating
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int BeerID { get; set; }
        public Beer Beer { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
    }
}
