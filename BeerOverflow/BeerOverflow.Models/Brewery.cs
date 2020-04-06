using System;
using System.ComponentModel.DataAnnotations;

namespace BeerOverflow.Models
{
    public class Brewery
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date)]
        public DateTime ModifiedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }





    }
}