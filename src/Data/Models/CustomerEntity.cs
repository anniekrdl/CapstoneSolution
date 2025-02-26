
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("klant")]
    public class CustomerEntity
    {
        [Column("klant_id")]
        [Key, Required]
        public int? Id { get; set; }
        [Column("voornaam")]
        [Required, StringLength(50)]
        public required string Name { get; set; }
        [Column("achternaam")]
        [Required, StringLength(50)]
        public required string LastName { get; set; }
        [Column("gebruikersnaam")]
        [Required, Index(IsUnique = true)]
        public required string UserName { get; set; }
        [Column("email")]
        [Required, StringLength(50)]
        public required string Email { get; set; }
        [Column("straat")]
        [Required, StringLength(50)]
        public required string Street { get; set; }
        [Column("toevoeging")]
        public string? Addition { get; set; }
        [Column("huisnummer")]
        [Required, MinLength(1)]
        public int Number { get; set; }
        [Column("woonplaats")]
        [Required, StringLength(50)]
        public required string City { get; set; }
        //public string Role { get; init; } = "Customer";


        //EF Core vereist een parameterloze constructor
        public CustomerEntity() { }


        public CustomerEntity(int? id, string userName, string name, string lastname, string email, string street, int number, string city, string? addition = null)
        {
            Id = id;
            Name = name;
            LastName = lastname;
            UserName = userName;
            Email = email;
            Street = street;
            Number = number;
            Addition = addition;
            City = city;
        }

    }
}