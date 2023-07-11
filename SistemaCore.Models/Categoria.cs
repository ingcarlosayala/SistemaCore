using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Categoria  es requerida")]
        [Display(Name = "Categoria")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Estado es requerida")]
        public bool Estado { get; set; }
    }
}
