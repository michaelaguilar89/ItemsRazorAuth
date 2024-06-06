using ItemsBlazorAuthApp.Data;
using ItemsBlazorAuthApp.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using SharedProyect.Dto;
using System.Collections.Generic;


namespace ItemsBlazorAuthApp.Service
{

    
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> Create(ProductInsertDto productDto,string UserId)
        {
            try
            {

                var item = _context.Products.FirstOrDefault(x => x.Name.Equals(productDto.Name));
                if (item == null)
                {
                    Product product = new();
                    // Asignación directa de datos del formulario a product
                    product.Name = productDto.Name;
                    product.Description = productDto.Description;
                    product.Quantity = productDto.Quantity;
                    product.Price = productDto.Price;
                    product.Comments = productDto.Comments;
                    product.CreationDate = DateTime.Now;
                    product.UserId = UserId;
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                    return "1";
                }
                else
                {
                    return "Concurrency error, product name already exists.";
                }

                }
            catch (Exception e)
            {

                return e.Message;
            }
        }
        public async Task<List<ProductDto>> Get(string? search)
        {
            List<ProductDto> list = new();
            if (!string.IsNullOrEmpty(search))
            {
                list = await _context.Products
                    .Include(p => p.User)
                    .Where(p => p.Name.Contains(search) || p.Description.Contains(search))
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
                    }).ToListAsync();
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
                        Price = p.Price,
                        Quantity = p.Quantity,
                        Comments = p.Comments,
                        CreationDate = p.CreationDate,
                        UserName = p.User.UserName
                    }).ToListAsync();
            }
            return list;
        }

        public async Task<ProductDto> GetById(int? id)
        {
            ProductDto productDto = new();

            productDto = await _context.Products
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
                       UserId = p.User.Id,
                       UserName = p.User.UserName
                   }).FirstOrDefaultAsync();
            return productDto;
        }


        public async Task<string> Update(ProductDto productDto)
        {
            try
            {
                Product product = new();
                Console.WriteLine("Post is Ok...");
                product.Id = productDto.Id;
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.Quantity = productDto.Quantity;
                product.Comments = productDto.Comments;
                product.CreationDate = productDto.CreationDate;
                product.UserId = productDto.UserId;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return "1";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }  

        public async Task<string> Delete(int id)
        {
            try
            {
                var data = await _context.Products.FindAsync(id);
                if (data !=null)
                {
                    _context.Products.Remove(data);
                    await _context.SaveChangesAsync();
                    return "1";
                }
                return "Not found!";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
    }
}
