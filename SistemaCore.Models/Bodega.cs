using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore.Models
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bodega es requerida")]
        [Display(Name = "Bodega")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion es requerida")]
        [MaxLength (200)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado es requerida")]
        public bool Estado { get; set; }
    }
}
