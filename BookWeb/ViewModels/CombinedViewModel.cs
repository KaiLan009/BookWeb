using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksWeb.ViewModels
{
    public class CombinedViewModel
    {
        public List<BooksViewModel> Books { get; set; }
        public List<AuthorsViewModel> Authors { get; set; }

        public BooksViewModel AddBook { get; set; } = new BooksViewModel();
        public BooksViewModel EditBook { get; set; } = new BooksViewModel();
        public AuthorsViewModel AddAuthor { get; set; } = new AuthorsViewModel();

    }
}
