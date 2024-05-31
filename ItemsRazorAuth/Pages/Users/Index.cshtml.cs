using ItemsRazorAuth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItemsRazorAuth.Pages.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string ErrorMessage { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<IdentityUser> Users { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                Users = await _context.Users.ToListAsync();
                
             
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }

        }
    }
}
