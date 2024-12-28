using BooksData.Entities;
using BooksWeb.Services;

namespace BooksWeb.Controllers
{
    public class AccountController : Controller
    {
        //Definicion de servicios
        private IBooksService svc;
        public AccountController(IBooksService svc)
        {
            this.svc = svc;
        }

        //Vista Inicio de sesion GET
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }

        //Acciones para inicio de sesion con validaciones
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel vm)
        {
            Users user = await svc.GetUserByEmail(vm.Email);
            if (user != null)
            {
                if (user.InactiveDate != null)
                {
                    ModelState.AddModelError("", "El usuario se encuentra dado de baja");
                    return View(vm);
                }
                if (user.UserPassword == vm.Password)
                {
                    return await Claims(user.UserID, vm.ReturnUrl);
                }
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            }
        return View(vm);
        }

        //Claims para inicio de sesion
        [AllowAnonymous]
        private async Task<IActionResult> Claims(int userId, string returnUrl)
        {
            UsersViewModel user=await svc.GetUserById(userId);
            var claims = new List<Claim>
            {
                new Claim("UserId",user.UserID.ToString(),ClaimValueTypes.Integer),
                new Claim("Name",user.UserName),
            };
            var claimsIdentity = new ClaimsIdentity(claims, "BooksWeb");
            await HttpContext.SignInAsync("BooksWeb", new ClaimsPrincipal(claimsIdentity));
            if(returnUrl != null && Url.IsLocalUrl(returnUrl))
            {
                returnUrl = returnUrl.Replace("/Search","");
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction("Index", "Books");
        }
    }
}