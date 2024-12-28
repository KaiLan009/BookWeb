﻿using BooksData;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BooksWeb.ViewModels;
using BooksData.Entities;

namespace BooksWeb.Services
{
    public interface IBooksService
    {
        Task<List<BooksViewModel>> GetBooksAsync();
        Task AddBooksAsync(BooksViewModel book);
        Task EditBookAsync(BooksViewModel book);
        Task DeleteBookAsync(int bookid);
        Task<BooksViewModel> GetBooksById(int id);
        Task<Users> GetUserByEmail(string emailuser);
        Task<UsersViewModel> GetUserById(int userId);

        // Clase que define los servicios context DB y mappers 
        public class BooksService : IBooksService
        {
            private readonly DbContextBooks ctx;
            private readonly IMapper mapper;

            //Clase que genera ctx y mapper 
            public BooksService(DbContextBooks ctx, IMapper mapper)
            {
                this.ctx = ctx;
                this.mapper = mapper;
            }

            //Servico que genera la consulta inicial y su mapeo solo muestra las lineas con InactiveDate nulo
            //en index a traves del controlador
            public async Task<List<BooksViewModel>> GetBooksAsync()=>
                mapper.Map<List<BooksViewModel>>(await ctx.Books.Where(b => b.InactiveDate == null).ToListAsync());

            //Servicio para agregar un nuevo libro
            public async Task AddBooksAsync(BooksViewModel vm)
            {
                Books book = new Books
                {
                    BookID=vm.BookID,
                    Title=vm.Title,
                    Year=vm.Year,
                    AuthorID=vm.AuthorID,
                    ISBN=vm.ISBN,
                    Editorial=vm.Editorial,
                    Pages=vm.Pages,
                    Format=vm.Format,
                    Synopsis=vm.Synopsis,
                    CreationDate= DateTime.Now,
                    CreationIDUser = 1
                };
                ctx.Books.Add(book);
                await ctx.SaveChangesAsync();
            }

            //Servicio para editar libro
            public async Task EditBookAsync(BooksViewModel vm)
            {
                Books book = await ctx.Books.FirstOrDefaultAsync(b=>b.BookID == vm.BookID);
                book.Title=vm.Title;
                book.Year=vm.Year;
                book.ISBN=vm.ISBN;
                book.Editorial=vm.Editorial;
                book.Pages=vm.Pages;
                book.Format=vm.Format;
                book.Synopsis=vm.Synopsis;
                await ctx.SaveChangesAsync();
            }

            //Servicio para eliminar libro
            public async Task DeleteBookAsync(int bookId)
            {
                Books book = await ctx.Books.FirstOrDefaultAsync(b=>b.BookID ==bookId);
                book.InactiveDate= DateTime.Now;
                await ctx.SaveChangesAsync();
            }
            //Servicio para obtener libro para editar desde la vista principal
            public async Task<BooksViewModel> GetBooksById(int idBook) =>
                mapper.Map<BooksViewModel>(await ctx.Books.FirstOrDefaultAsync(u => u.BookID == idBook));

            //Servicio para obtener usuario para iniciar sesion
            public async Task<UsersViewModel> GetUserById(int idUser) =>
                mapper.Map<UsersViewModel>(await ctx.Users.FirstOrDefaultAsync(u=>u.UserID==idUser));

            //Servicio para comprobar email de usuario
            public async Task<Users> GetUserByEmail(string emailuser) =>
                await ctx.Users.FirstOrDefaultAsync(u => u.UserEmail == emailuser);

            
        }
    }
}