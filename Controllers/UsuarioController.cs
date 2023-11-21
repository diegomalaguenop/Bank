using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Bank.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace Bank.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private MyContext _context;

        public UsuarioController(ILogger<UsuarioController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [SessionCheck]
        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        public IActionResult ShowUserDetails()
        {
            var nombreUsuario = HttpContext.Session.GetString("Nombre");
            var emailUsuario = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(emailUsuario))
            {
                return RedirectToAction("Login", "Usuario");
            }

            ViewBag.NombreUsuario = nombreUsuario;
            ViewBag.EmailUsuario = emailUsuario;

            return View();
        }



        [HttpGet]
        [Route("registro")]
        public IActionResult Registro()
        {
            return RedirectToAction("Dashboard", "Home");
        }

   
        [HttpPost]
        [Route("/")]
        public IActionResult ProcesaRegistro(Usuario NuevoUsuario)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<Usuario> Hasher = new PasswordHasher<Usuario>();
                NuevoUsuario.Password = Hasher.HashPassword(NuevoUsuario, NuevoUsuario.Password);
                _context.Usuarios.Add(NuevoUsuario);
                _context.SaveChanges();

                return RedirectToAction("Index", "Transaccion");
            }
            else
            {
                ModelState.AddModelError("Nombre", "Este campo es obligatorio.");
                return View("Registro", NuevoUsuario);
            }
        }


        [HttpGet]
        [Route("procesa/logout")]
        public IActionResult ProcesaLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

            [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View(); 
        }

        [HttpPost]
        [Route("procesa/login")]
        public IActionResult ProcesaLogin(Login login)
        {
            if(ModelState.IsValid)
            {
                Usuario usuario = _context.Usuarios.FirstOrDefault(user => user.Email == login.Email);

                if(usuario != null)
                {
                    PasswordHasher<Login> Hasher = new PasswordHasher<Login>();
                    var result = Hasher.VerifyHashedPassword(login, usuario.Password, login.Password);

                    if(result != 0)
                    {
                        HttpContext.Session.SetString("Nombre", usuario.Nombre);
                        HttpContext.Session.SetString("Apellido", usuario.Apellido);
                        HttpContext.Session.SetString("Email", usuario.Email);
                        HttpContext.Session.SetInt32("Id", usuario.UsuarioId);
                        
                         return RedirectToAction("Index", "Transaccion");
                    }
                }
                ModelState.AddModelError("Password", "Credenciales incorrectas");
                return View("Index");
            }
            return View("Index");
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


     
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string email = context.HttpContext.Session.GetString("Email");
            if (email == null)
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
        }
    }
}
