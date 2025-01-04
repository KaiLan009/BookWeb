using AutoMapper;
using BooksData;
using BooksWeb.Services;
using Microsoft.EntityFrameworkCore;

namespace BooksWeb.Controllers
{
    public class BooksController : Controller
    {
        //Definir y agregar los servicios
        private IBooksService svc;
        private readonly DbContextBooks ctx;

        public BooksController(IBooksService svc)
        {
            this.svc = svc;
            this.ctx = ctx;
        }
        //Cierre de sesion
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("BooksWeb");
            return RedirectToAction("LogIn", "Account");
        }

        /*/GET Vista principal
        public async Task<IActionResult> Index()
        {
            List<BooksViewModel> books = await svc.GetBooksAsync();
            return View(books);
        }*/

        //Get Vista principal con consultas combinadas
        public async Task<IActionResult> Index()
        {
            // Obtener datos del servicio
            List<BooksViewModel> books = await svc.GetBooksAsync();
            List<AuthorsViewModel> authors = await svc.GetAuthorsAsync();

            // Combinar datos en el ViewModel
            var model = new CombinedViewModel
            {
                Books = books,
                Authors = authors
            };

            // Pasar el modelo combinado a la vista
            return View(model);
        }

        //GET agregar libro
        public IActionResult AddBook()
        {
            return View();
        }

        //POST Agregar libro redireccion a vista
        [HttpPost]
        public async Task<IActionResult> AddBook(BooksViewModel book)
        {
            await svc.AddBooksAsync(book);
            return RedirectToAction("Index");
        }

        //GET Editar libro
        public async Task<IActionResult> EditBook(int id)
        {
            BooksViewModel book = await svc.GetBooksById(id);
            return View(book);
        }

        //POST Editar libro y redireccion a vista
        [HttpPost]
        public async Task<IActionResult> EditBook(BooksViewModel book)
        {
            await svc.EditBookAsync(book);
            return RedirectToAction("Index");
        }

        //POST Eliminar de registro de la vista
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBook(int bookid)
        {
            await svc.DeleteBookAsync(bookid);
            return RedirectToAction("Index");
        }
    }
}
