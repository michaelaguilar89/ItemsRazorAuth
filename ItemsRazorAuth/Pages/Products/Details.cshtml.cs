using ItemsRazorAuth.Data;
using SharedProyect.Dto;
using SharedProyect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItemsRazor.Pages.Products
{

    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ProductDto product{get;set;}
       
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
            if (product ==null)
            {
                ErrorMessage = "Product Not Found!";
            }
           
        }

        
    }
}
