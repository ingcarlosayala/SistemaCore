using Microsoft.AspNetCore.Mvc;
using SistemaCore.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore.Models;
using SistemaCore.Utilidades;

namespace SistemaCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public CategoriasController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Categoria.Agregar(categoria);
                await unidadTrabajo.Guardar();
                TempData[DS.Exitosa] = "Categoria Creada Exitoxamente";
                return RedirectToAction("Index");
            }

            TempData[DS.Error] = "Error al Crear la categoria";

            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var categoriaDb = await unidadTrabajo.Categoria.Obtener(id);

            if (categoriaDb == null)
            {
                return NotFound();
            }

            return View(categoriaDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Categoria.Actualizar(categoria);
                await unidadTrabajo.Guardar();
                TempData[DS.Exitosa] = "Categoria Actualizada Exitoxamente";
                return RedirectToAction("Index");
            }

            TempData[DS.Error] = "Error al Actualizar la categoria";

            return View(categoria);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Json(new { data = await unidadTrabajo.Categoria.ObtenerTodos() });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoriaDb = await unidadTrabajo.Categoria.Obtener(id);

            if (categoriaDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            unidadTrabajo.Categoria.Remover(categoriaDb);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria eliminada exitosamente" });
        }

        #endregion
    }
}
