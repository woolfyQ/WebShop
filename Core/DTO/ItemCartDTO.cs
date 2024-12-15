using Core.Entity;

namespace Core.DTO
{
    public class ItemCartDTO
    {
        public Guid ProductId { get; set; }  
        public string ProductName { get; set; }
        public int Quantity { get; set; }   
        public decimal Price { get; set; }     
        public Cart Cart { get; set; }

    }
}

