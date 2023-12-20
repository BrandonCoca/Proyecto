using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Context;

namespace Proyecto.Controllers
{
    public class LoginController : Controller
    {
        MiContext _miContext;
        //ctor
        public LoginController(MiContext miContext)
        {
            _miContext = miContext;
        }
        //GET
        public IActionResult Index()
        {
            //pantalla del login
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(string correo, string contrasena)
        {
            var usuario = await _miContext.Usuarios
                .Where(x => x.Email == correo && x.Password == contrasena)
                .FirstOrDefaultAsync();
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




    }
}
