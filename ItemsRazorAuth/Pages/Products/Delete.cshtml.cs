using ItemsRazorAuth.Data;
using ItemsRazorAuth.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItemsRazorAuth.Pages.Products
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductDto product { get; set; }

        public string ErrorMessage { get; set; }
        public async Task OnGetAsync(int id)
        {


            product = await _context.Products
             .Include(p => p.User)
             .Where(p => p.Id == id)
             .Select(p => new ProductDto
             {
                 Id = p.Id,
                 Name = p.Name,
                 Description = p.Description,
                 Price = p.Price,
                 Quantity = p.Quantity,
                 Comments = p.Comments,
                 CreationDate = p.CreationDate,
                 UserName = p.User.UserName
             })
              .FirstOrDefaultAsync();
            if (product == null)
            {
                ErrorMessage = "Product Not Found!";
            }

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                var item = await _context.Products.FindAsync(id);
                if (item!=null)
                {
                    _context.Products.Remove(item);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Products/Index");
                }
                ErrorMessage = "Concurrency Exception, Product not found!";
                return Page();
            }
            catch (Exception e)
            {

                ErrorMessage = e.Message;
                return Page();
            }
        }
    }
}
