#pragma warning disable CS8618
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class Transaccion
    {
        [Key]
        public int TransaccionId { get; set; }

 
        [Required(ErrorMessage = "El monto es obligatoria.")]
        public string Monto { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; } // Declarada como nullable
    }
}
