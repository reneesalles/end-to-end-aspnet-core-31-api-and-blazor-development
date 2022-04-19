using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.API.Data.DTOs.Author;
using BookStoreApp.API.Data.DTOs.Book;
using BookStoreApp.API.Data.Models;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            MapAuthorDTOs();
            MapBookDTOs();
        }

        private void MapAuthorDTOs()
        {
            CreateMap<Author, AuthorReadDTO>();
            
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
        }

        private void MapBookDTOs()
        {
            CreateMap<Book, BookReadDTO>()
                .ForMember(
                    q => q.AuthorName, 
                    d => d.MapFrom(b => b.Author == null ? null : $"{b.Author.FirstName} {b.Author.LastName}"));
            CreateMap<Book, BookDetailsDTO>()
                .ForMember(
                    q => q.AuthorName, 
                    d => d.MapFrom(b => b.Author == null ? null : $"{b.Author.FirstName} {b.Author.LastName}"));

            CreateMap<BookCreateDTO, Book>().ReverseMap();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();
        }
    }
}