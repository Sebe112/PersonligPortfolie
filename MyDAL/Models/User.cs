using Microsoft.AspNetCore.Identity;

namespace MyDAL.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; } = "User";
    }
}
