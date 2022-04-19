using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.API.Data.DTOs.Author
{
    public class AuthorReadDTO : BaseDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
    }
}