using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCore.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore.Data;
using SistemaCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore.AccesoDatos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Categoria categoria)
        {
            var categoriaDb = await dbContext.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);

            if (categoriaDb != null)
            {
                categoriaDb.Nombre = categoria.Nombre;
                categoriaDb.Estado = categoria.Estado;
            }
        }

        public IEnumerable<SelectListItem> CategoriaLista()
        {
            return dbContext.Categorias.Where(c => c.Estado == true).ToList().Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });
        }
    }
}
