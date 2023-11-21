using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Por favor proporciona este dato")]
        [MinLength(3, ErrorMessage = "El nombre de usuario debe tener al menos 2 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor proporciona este dato")]
        [MinLength(3, ErrorMessage = "El apellido de usuario debe tener al menos 2 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Por favor proporciona este dato")]
        [EmailAddress(ErrorMessage = "Por favor proporciona un correo válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor proporciona este dato")]
        [MinLength(8, ErrorMessage = "Tu contraseña debe tener al menos 8 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string PasswordConfirm { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

                public ICollection<Transaccion>? Transacciones { get; set; } = new List<Transaccion>();


    }
}
