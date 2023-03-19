using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // markup our class here to effect the sql generation process
using Microsoft.AspNetCore.Identity;

namespace ContactPro_MVC.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")] // Allows for spaces
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and a max {1} characters long.", MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and a max {1} characters long.", MinimumLength = 2)]
        public string? LastName { get; set; }

        [NotMapped] // Won't be in the database, calculated at runtime
        public string? FullName { get { return $"{FirstName} {LastName}"; } }

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public virtual ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    }
}
