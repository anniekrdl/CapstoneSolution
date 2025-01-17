namespace Core.Models
{

    public class Product
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; } //in cents
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }

        public Product(int? id, string name, string description, int price, int stock, int categoryId, string imageUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            ImageUrl = imageUrl;

        }


    }

}