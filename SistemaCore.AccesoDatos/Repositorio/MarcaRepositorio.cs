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
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public MarcaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Marca marca)
        {
            var marcaDb = await dbContext.Marcas.FirstOrDefaultAsync(c => c.Id == marca.Id);

            if (marcaDb != null)
            {
                marcaDb.Nombre = marca.Nombre;
                marcaDb.Estado = marca.Estado;
            }
        }

        public IEnumerable<SelectListItem> MarcaLista()
        {
            return dbContext.Marcas.Where(m => m.Estado == true).ToList().Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });
        }
    }
}
