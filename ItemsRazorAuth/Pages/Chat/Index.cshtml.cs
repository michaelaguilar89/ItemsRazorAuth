using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemsRazorAuth.Pages.Chat
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet( )
        {
        }
    }
}
