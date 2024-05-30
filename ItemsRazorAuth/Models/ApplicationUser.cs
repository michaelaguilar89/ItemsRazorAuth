using Microsoft.AspNetCore.Identity;

namespace ItemsRazorAuth.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string CretionDate { get; set; }
        public string LastActivitiDate { get; set; }
    }
}
