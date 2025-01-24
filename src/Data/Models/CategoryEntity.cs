using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{

    [Table("categorie")]
    public class CategoryEntity
    {

        [Column("categorie_id")]
        [Key, Required]
        public int? Id { get; set; }
        [Column("naam")]
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Column("beschrijving")]
        [Required, StringLength(255)]
        public string Description { get; set; }

        public CategoryEntity(int? id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

        }
    }

}