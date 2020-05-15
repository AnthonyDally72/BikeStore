using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Employee")]
        public string Name { get; set; }

        [NotMapped]
        public bool IsSuperEmployee { get; set; }
    }
}
