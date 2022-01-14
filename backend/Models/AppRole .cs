using Microsoft.AspNetCore.Identity;

namespace dotnet_rpg.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }
        // extra properties here 
    }
}