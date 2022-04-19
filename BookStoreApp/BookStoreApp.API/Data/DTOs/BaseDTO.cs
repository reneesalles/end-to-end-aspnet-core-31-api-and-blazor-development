using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.API.Data.DTOs
{
    public abstract class BaseDTO
    {
        [Required]
        public int Id { get; set; }
    }
}