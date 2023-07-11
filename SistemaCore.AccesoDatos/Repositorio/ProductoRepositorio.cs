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
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public ProductoRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Producto producto)
        {
            var productoDb = await dbContext.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id);

            if (productoDb != null)
            {
                productoDb.Descripcion = producto.Descripcion;
                productoDb.Costo = producto.Costo;
                productoDb.Precio = producto.Precio;
                productoDb.ImagenUrl = producto.ImagenUrl;
                productoDb.Estado = producto.Estado;
                productoDb.CategoriaId = producto.CategoriaId;
                productoDb.MarcaId = producto.MarcaId;
            }
        }
    }
}
