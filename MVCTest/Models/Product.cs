using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MVCTest.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required, StringLength(250, ErrorMessage = "Description should not be greater than 250 words.")]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}

