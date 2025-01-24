using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.EF;


[Table("product")]
public class ProductEntityEF
{
    [Column("product_id")]
    [Key, Required]
    public int Id { get; set; }
    [Column("naam"), MinLength(2), MaxLength(50)]
    [Required,]
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
    [Required]
    public string ImageUrl { get; set; }


}
