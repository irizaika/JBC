using Microsoft.AspNetCore.Identity;

namespace JBC.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
