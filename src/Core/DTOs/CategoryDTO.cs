namespace Core.DTOs
{

    public class CategoryDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryDTO(int? id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

        }
    }

}