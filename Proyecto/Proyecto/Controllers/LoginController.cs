using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Context;

namespace Proyecto.Controllers
{
    public class LoginController : Controller
    {
        MiContext _miContext;
        public LoginController(MiContext miContext)
        {
            _miContext = miContext;
        }
        public IActionResult Index()
        {
            // para la pantalla de login
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            var usuario = await _miContext.Usuarios
                .Where(x => x.Email == correo && x.Password == contrasena)
                .FirstOrDefaultAsync();
            if (usuario == null)// se ha controlado
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["LoginError"] = "Cuenta y passgord incorrecto";
                return Redirect("Index");
            }
        }

    }
}
