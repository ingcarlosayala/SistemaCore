using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore.Models
{
    public class UsuarioAplicacion : IdentityUser
    {
        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ciudad es requerida")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Pais es requerido")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "Direccion es requerido")]
        public string Direccion { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
