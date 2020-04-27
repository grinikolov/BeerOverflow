using System;
using System.ComponentModel.DataAnnotations;

namespace BeerOverflow.Models
{
    public class BeerStyle
    {
        public BeerStyle()
        {
        }
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}