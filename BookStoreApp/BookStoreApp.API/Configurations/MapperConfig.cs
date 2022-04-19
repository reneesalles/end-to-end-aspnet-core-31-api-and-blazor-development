using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.API.Data.DTOs.Author;
using BookStoreApp.API.Data.Models;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            MapAuthorDTOs();
        }

        private void MapAuthorDTOs()
        {
            CreateMap<AuthorReadDTO, Author>().ReverseMap();
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
        }
    }
}