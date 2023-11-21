using System.ComponentModel.DataAnnotations;

public class Login
{
    [Required(ErrorMessage = "Por favor proporciona este dato.")]
    [EmailAddress(ErrorMessage = "Por favor proporciona un correo válido.")]
    [EmailValidation(ErrorMessage = "Por favor proporciona un correo válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Por favor proporciona este dato.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    public string Password { get; set; }
}
