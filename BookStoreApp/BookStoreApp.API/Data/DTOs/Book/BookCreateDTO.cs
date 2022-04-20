using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.API.Data.DTOs.Book
{
    public class BookCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [Range(1000, int.MaxValue)]
        public int Year { get; set; }
        [Required]
        [StringLength(50)]
        public string Isbn { get; set; } = null!;
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Summary { get; set; } = null!;
        [StringLength(50)]
        public string? Image { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}