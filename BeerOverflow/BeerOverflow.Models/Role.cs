using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BeerOverflow.Models
{
    public class Role :IdentityRole<int>
    {
        //[Key]
        //public int Id { get; set; }

        //[Required]
        //public string Name { get; set; }

        public ICollection<User> Users { get; set; }

    }
}