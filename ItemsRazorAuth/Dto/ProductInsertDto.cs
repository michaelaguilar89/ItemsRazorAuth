using System.ComponentModel.DataAnnotations;

namespace ItemsRazorAuth.Dto
{
    public class ProductInsertDto
    {


        
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

    }
}
