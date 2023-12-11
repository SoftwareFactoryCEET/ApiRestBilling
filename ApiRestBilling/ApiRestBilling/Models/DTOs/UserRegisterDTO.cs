using System.ComponentModel.DataAnnotations;

namespace ApiRestBilling.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage ="El correo de usuario es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El apellido de usuario es requerido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El password es requerido")]
        public string Password { get; set; }      
        public List<string> Roles { get; set; } // Changed to List for multiple roles
    }
}
