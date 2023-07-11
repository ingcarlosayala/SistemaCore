using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio : IRepositorio<Categoria>
    {
        Task Actualizar(Categoria categoria);
        IEnumerable<SelectListItem> CategoriaLista();
    }
}
