using System;
using BooksData.Entities;
using AutoMapper;
using BooksWeb.ViewModels;

namespace BooksWeb
{
    public class AutoMapperMappings : Profile
    {
        public AutoMapperMappings() 
        {
            CreateMap<Books, BooksViewModel>().ReverseMap();
        }
    }
}
