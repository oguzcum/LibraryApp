using Microsoft.AspNetCore.Identity;

namespace LibraryApp1.Models
{
    public class UserInfo : IdentityUser
    {
        public string FirstName { get; set; }

        public ICollection<UserProduct> UserProducts { get; set; }

        
    }
}