using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using BooksData.Entities;

namespace BooksWeb.ViewModels
{
    public class BooksViewModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AuthorID { get; set; }
        public int? ISBN { get; set; }
        public string Editorial { get; set; }
        public int Pages { get; set; }
        public string Format { get; set; }
        public string Synopsis { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreationIDUser { get; set; }
        public DateTime? InactiveDate { get; set; }
    }
}
