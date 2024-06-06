using ItemsBlazorAuthApp.Data;
using System.ComponentModel.DataAnnotations;

namespace ItemsBlazorAuthApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public string? UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
