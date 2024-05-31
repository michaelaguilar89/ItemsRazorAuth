
using ItemsRazorAuth.Data;
using ItemsRazorAuth.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace ItemsRazor.Pages.Products
{
   
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
          
        }
        [BindProperty(SupportsGet =true)]
        public IList<ProductDto> list { get; set; }
        [BindProperty(SupportsGet = true)]
        public string search { get; set; }
        public string ErrorMessage { get; set; }
        public string TitleView { get; set; }

        public PaginatedList<ProductDto> Products { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(search))
                {
                    
                    list = await _context.Products
                        .Include(p => p.User)
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
                               .Where(
                                        x=>x.Name.Contains(search)||
                                        x.Description.Contains(search)
                                      
                                      )
                                      .ToListAsync();
                    if (list == null || list.Count == 0)
                    {
                        ErrorMessage = "Data Not Found,try again later...";
                        return Page();
                    }
                }
                else
                {
                    list = await _context.Products
                                .Include(p => p.User)
                                .Select(p => new ProductDto
                               {
                                 Id = p.Id,
                                 Name = p.Name,                                 
                                 Description = p.Description,
                                 Price=p.Price,
                                 Quantity=p.Quantity,
                                 Comments=p.Comments,
                                 CreationDate=p.CreationDate,
                                 UserName = p.User.UserName

                                }).ToListAsync();
                    if (list == null || list.Count == 0)
                    {
                        ErrorMessage = "Data Not Found,try again later...";
                        return Page();
                    }
                }
               
            }
            catch (Exception e)
            {

                ErrorMessage=e.Message;
                return Page();
            }
            return Page();

        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var item =await _context.Products.FindAsync(id);
                if (item!=null)
                {
                    _context.Products.Remove(item);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Products/Index");
                }
                ErrorMessage = "Concurrency Exeption, Product Not found!";
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
