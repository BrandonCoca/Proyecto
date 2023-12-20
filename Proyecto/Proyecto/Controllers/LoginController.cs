using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Context;

namespace Proyecto.Controllers
{
    public class LoginController : Controller
    {
        MiContext _miContext;
<<<<<<< HEAD
        //ctor
=======
>>>>>>> 7a66f1b2e7398d30121848f85cb34e1f05edec1b
        public LoginController(MiContext miContext)
        {
            _miContext = miContext;
        }
<<<<<<< HEAD
        //GET
        public IActionResult Index()
        {
            //pantalla del login
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(string correo, string contrasena)
=======
        public IActionResult Index()
        {
            // para la pantalla de login
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena)
>>>>>>> 7a66f1b2e7398d30121848f85cb34e1f05edec1b
        {
            var usuario = await _miContext.Usuarios
                .Where(x => x.Email == correo && x.Password == contrasena)
                .FirstOrDefaultAsync();
<<<<<<< HEAD
            if(usuario == null) 
            {
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Create", "Usuarios");
            }
            else
            {
                TempData["LoginError"] = "Cuenta y password incorrecto";
               return Redirect("Index");
               // return RedirectToAction("Create", "Usuarios");
            }
        }
        public async Task<ActionResult> Regis()
        {
            
                 return RedirectToAction("Create", "Usuarios");
            
        }



=======
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
>>>>>>> 7a66f1b2e7398d30121848f85cb34e1f05edec1b

    }
}
