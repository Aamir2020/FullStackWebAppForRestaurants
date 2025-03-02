using Microsoft.AspNetCore.Identity;

namespace FullStackWebAppForRestaurants.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }

    }
}
