
using ItemsRazorAuth.Data;
using ItemsRazorAuth.Dto;
using ItemsRazorAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ItemsRazor.Pages.Products
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ProductDto product { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        public int UpdateQuantity { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
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
                    ErrorMessage = "Not Found!";
                    return Page();
                }

                return Page();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return Page();
            }
           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var data = await _context.Products.FindAsync(product.Id);
                if (data!=null)
                {
                    data.Name = product.Name;
                    data.Description = product.Description;
                    data.Price = product.Price;
                    data.Comments = product.Comments;
                        //suma
                    data.Quantity += UpdateQuantity;
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Products/Index");
                }
                ErrorMessage = "Not Found";
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
