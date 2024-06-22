using System.ComponentModel.DataAnnotations;

namespace Organizer.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public Category(int  id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
