using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{

    [Table("product")]
    public class ProductEntity
    {
        [Column("product_id")]
        [Key, Required]
        public int? Id { get; set; }
        [Column("naam")]
        [Required, MinLength(2), MaxLength(50)]
        public string Name { get; set; }
        [Column("beschrijving")]
        [Required, MinLength(2)]
        public string Description { get; set; }
        [Column("prijs")]
        [Required]
        public int Price { get; set; } //in cents'
        [Column("voorraad")]
        [Required]
        public int Stock { get; set; }
        [Column("categorie_id")]
        [ForeignKey("categorie"), Required]
        public int CategoryId { get; set; }
        [Column("afbeelding_url")]
        [Required, StringLength(50)]
        public string ImageUrl { get; set; }



        public ProductEntity(int? id, string name, string description, int price, int stock, int categoryId, string imageUrl)
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