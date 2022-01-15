using Microsoft.AspNetCore.Identity;

namespace Sharperio.Models
{
    public class AppUser : IdentityUser
    {
        //add your custom properties which have not included in IdentityUser before
        public List<Character> Characters { get; set; }
    }
}