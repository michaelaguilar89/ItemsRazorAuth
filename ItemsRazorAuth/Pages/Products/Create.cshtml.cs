
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Microsoft.EntityFrameworkCore;
using ItemsRazorAuth.Models;
using Microsoft.AspNetCore.Identity;
using ItemsRazorAuth.Data;
using Microsoft.AspNetCore.Authorization;
using ItemsRazorAuth.Dto;

namespace ItemsRazor.Pages.Products
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ApplicationDbContext context
                           , UserManager<IdentityUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }


        
        //public Product product { get; set; }
        [BindProperty]
        public ProductInsertDto productDto { get; set; }


        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Model Is Invalid!";
                    return Page();
                }
         
         
                var item = await _context.Products.FirstOrDefaultAsync(x => x.Name.Equals(productDto.Name));
                if (item==null)
                {
                    Product product = new();
                    product.Name = productDto.Name;
                    product.Description = productDto.Description;
                    product.Quantity = productDto.Quantity;
                    product.Price = productDto.Price;
                    product.Comments = productDto.Comments;
                    product.CreationDate = DateTime.Now;
                    var user = await _userManager.GetUserAsync(User);

                    product.UserId = user.Id;
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Products/Index");

                }ErrorMessage = "concurrency error, product name already exists";
                return Page();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :" + e.Message);
                ErrorMessage = e.Message;
                return Page();
            }
        }
    }
}
