namespace SharedProyect.Dto
{
    public class ProductDto
    {
       
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string Description { get; set; }
       
        public int Price { get; set; }
        
        public int Quantity { get; set; }
       
        public string Comments { get; set; }
       
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        
    }
}
