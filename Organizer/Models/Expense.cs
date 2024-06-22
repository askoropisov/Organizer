using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
