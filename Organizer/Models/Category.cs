using System.ComponentModel.DataAnnotations;

namespace Organizer.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public Category(string? name)
        {
            Name = name;
        }
    }
}
