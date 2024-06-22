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

        public Expense(string? category, string? description, int value, DateTime date)
        {
            Category = category;
            Description = description;
            Value = value;
            Date = date;
        }
    }
}
