using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Entities
{
    public class Item 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

       
    }
}