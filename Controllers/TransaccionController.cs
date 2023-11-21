using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using System;
using System.Linq;

namespace Bank.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly MyContext _context;

        public TransaccionController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Obtener todas las transacciones ordenadas por fecha descendente
            var transacciones = _context.Transacciones.OrderByDescending(t => t.CreatedAt).ToList();
            return View(transacciones);
        }

        [HttpGet]
        public IActionResult Deposito()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Deposito(decimal monto)
        {
            if (monto <= 0)
            {
                ModelState.AddModelError("monto", "El monto debe ser mayor que cero.");
                return View();
            }

            // Crear una nueva transacción de depósito
            var transaccion = new Transaccion
            {
                Monto = monto.ToString(), // Aquí se guarda como string, podrías cambiarlo a decimal si es más adecuado en tu caso
                CreatedAt = DateTime.Now,
                // También puedes asociar la transacción con el usuario actual si tienes un sistema de autenticación implementado
            };

            _context.Transacciones.Add(transaccion);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


private List<Transaccion> ObtenerTransacciones()
{
    // Aquí se realiza la lógica para obtener las transacciones desde tu contexto (_context)
    // Este es solo un ejemplo básico, podrías necesitar realizar una consulta más específica.
    return _context.Transacciones.ToList();
}
        public IActionResult VerTransacciones()
{
    string tuString = "ejemplo";
    decimal tuDecimal = 100.0M;

    // Lógica para obtener las transacciones
    var transacciones = ObtenerTransacciones(); // Método para obtener las transacciones

    var modelo = new Tuple<string, decimal, List<Bank.Models.Transaccion>>(tuString, tuDecimal, transacciones);

    return View(modelo);
}

        [HttpGet]
        public IActionResult Retiro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Retiro(decimal monto)
        {
            if (monto <= 0)
            {
                ModelState.AddModelError("monto", "El monto debe ser mayor que cero.");
                return View();
            }

            // Crear una nueva transacción de retiro
            var transaccion = new Transaccion
            {
                Monto = (-monto).ToString(), // El retiro se registra como un valor negativo
                CreatedAt = DateTime.Now,
                // También puedes asociar la transacción con el usuario actual si tienes un sistema de autenticación implementado
            };

            _context.Transacciones.Add(transaccion);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
