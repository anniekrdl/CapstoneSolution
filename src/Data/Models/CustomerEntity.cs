
namespace Data.Models
{
    public class CustomerEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }

        public string? Addition { get; set; }
        public int Number { get; set; }
        public string City { get; set; }

        public string Role { get; init; } = "Customer";



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