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
    public class UsuarioAplicacion : Repositorio<UsuarioAplicacion>, IUsuarioAplicacion
    {
        private readonly ApplicationDbContext dbContext;

        public UsuarioAplicacion(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
