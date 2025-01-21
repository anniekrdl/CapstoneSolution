namespace Data.Models
{

    public class CategoryEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryEntity(int? id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

        }
    }

}