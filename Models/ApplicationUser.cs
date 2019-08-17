using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moogle.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public ApplicationUserRole UserRole { get;set; }
    }

    [NotMapped]
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

    [NotMapped]
    public class ApplicationRole : IdentityRole
    {
        public ApplicationUserRole UserRole { get; set; }
    }
}
