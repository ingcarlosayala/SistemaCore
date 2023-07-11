using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Marca  es requerida")]
        [Display(Name = "Marca")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Estado es requerida")]
        public bool Estado { get; set; }
    }
}
