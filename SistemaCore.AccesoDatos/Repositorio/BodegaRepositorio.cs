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
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public BodegaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Bodega bodega)
        {
            var bodegaDb = await dbContext.Bodegas.FirstOrDefaultAsync(b => b.Id == bodega.Id);

            if (bodegaDb != null)
            {
                bodegaDb.Nombre = bodega.Nombre;
                bodegaDb.Descripcion = bodega.Descripcion;
                bodegaDb.Estado = bodega.Estado;
            }
        }
    }
}
