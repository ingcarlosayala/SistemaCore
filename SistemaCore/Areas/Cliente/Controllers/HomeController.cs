using Microsoft.AspNetCore.Mvc;
using SistemaCore.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore.Models;
using SistemaCore.Models.ViewsModels;
using System.Diagnostics;

namespace SistemaCore.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public HomeController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Producto> productoLista = await unidadTrabajo.Producto.ObtenerTodos();

            return View(productoLista);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}